import json
import numpy as np

# Verileri yükleme
with open("adverts.json", "r") as adverts_file:
    adverts = json.load(adverts_file)

with open("cv.json", "r") as cvs_file:
    cvs = json.load(cvs_file)

# Sayısal MSE hesaplama
def mse_numeric(actual, expected):
    return (actual - expected) ** 2 / max(expected, 1)  # max with 1 to avoid zero division

# Liste ve string MSE hesaplama
def mse_list(actual, expected):
    matched = len(set(actual).intersection(set(expected)))
    return (len(expected) - matched) ** 2 / max(len(expected), 1)

# CV ve Advert eşleştirme skoru hesaplama
def match_advert_with_cv(advert, cv):
    mse_values = []

    # Experience hesaplama
    total_experience_years = sum([int(company["Years"]) for company in cv["Companies"]])
    if advert.get("Experience") is not None:
        mse_values.append(mse_numeric(total_experience_years, advert["Experience"]))

    # CompanySize karşılaştırması
    if advert.get("CompanySize") and cv.get("PreviousJob") and "CompanySize" in cv["PreviousJob"]:
        mse_values.append(mse_numeric(cv["PreviousJob"]["CompanySize"], advert["CompanySize"]))

    # Skills karşılaştırması
    mse_values.append(mse_list(cv["Skills"], advert["Skills"]))

    # Education level karşılaştırması
    if cv["EducationInfo"]["EduLevel"] and advert["EducationLevel"]:
        mse_values.append(mse_numeric(int(cv["EducationInfo"]["EduLevel"]), int(advert["EducationLevel"])))

    # Languages karşılaştırması
    mse_values.append(mse_list(cv["Languages"], advert["Languages"]))

    # Genel MSE ortalaması
    mse_avg = np.mean(mse_values)
    success_rate = 1 - mse_avg  # Başarı oranı
    
    return success_rate

# CV'leri ilanlarla eşleştirme
def match_adverts_with_cvs(adverts, cvs, threshold=0.7):
    matches = []
    
    for advert in adverts:
        matched_cvs = []
        
        for cv in cvs:
            success_rate = match_advert_with_cv(advert, cv)
            if success_rate >= threshold:
                matched_cvs.append({"CVId": cv["Id"], "SuccessRate": success_rate})
        
        matches.append({
            "AdvertId": advert["Id"],
            "MatchedCVs": matched_cvs
        })
    
    with open("matches.json", "w") as matches_file:
        json.dump(matches, matches_file, indent=4)
    
    print("Eşleşmeler başarıyla kaydedildi.")

# Eşleştirmeleri çalıştır
match_adverts_with_cvs(adverts, cvs)
