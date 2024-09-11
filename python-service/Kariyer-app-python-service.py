import json
import requests
import os
from flask import Flask, jsonify

app = Flask(__name__)

# Eşleşme oranını hesaplayan fonksiyon
def calculate_success_rate(advert, cv):
    mse = 0
    total_weights = 0

    # Deneyim 
    weight = advert["experience"]["xpScale"]
    total_weights += weight
    total_experience_years = sum(int(company["years"]) for company in cv["companies"])
    print("yeardan önce")
    if advert["experience"]["xpReq"] and total_experience_years >= advert["experience"]["years"]:
        print("yeardan sonra")
        mse +=0
    elif (not advert["experience"]["xpReq"]):
        print("içeri year")
        if total_experience_years >= advert["experience"]["years"]:
            mse+=0
        else:
            diff=advert["experience"]["years"]-total_experience_years
            if advert["experience"]["years"]==0:
                err=diff/0.1

            else:
                err=diff/advert["experience"]["years"]
                
            mse += (err ** 2) * weight

    else:
        print("elseyeaar")
        mse+=10000000
        

###
    # Şirket büyüklüğü karşılaştırması (en büyük şirkete bakıyoruz)

    weight = advert["companySize"]["compScale"]
    total_weights += weight
    largest_company_size = max(int(company["companySize"]) for company in cv["companies"])
    print("sizedan önce")
    if advert["companySize"]["compReq"] and largest_company_size >= advert["companySize"]["size"]:
        print("siedan sonra")
        mse+=0
    elif (not advert["companySize"]["compReq"]):
        print("içeri size")
        if largest_company_size >= advert["companySize"]["size"]:
            mse+=0
        else:
            if advert["companySize"]["size"]<=largest_company_size:
                mse+=0
            else:
                diff = advert["companySize"]["size"] - largest_company_size
                if advert["companySize"]["size"]<=0:
                    err=diff/0.1
                else:
                    err=diff/advert["companySize"]["size"]
                mse += (err ** 2) * weight
            
    else:
        mse+=10000000
        


        
            
    
    # Maaş beklentisi karşılaştırması

    weight = advert["salary"]["salScale"]
    total_weights += weight
    print("maxdan önce")
    print(type(cv["expectedValues"]["expectedSalary"]))
    print(type(advert["salary"]["max"]))
    max_salary = advert["salary"]["max"] if advert["salary"]["max"] is not None else 0

    if advert["salary"]["salReq"] and cv["expectedValues"]["expectedSalary"] >= max_salary:
        print("maxdan sonra")
        mse+=0
    elif( not advert["salary"]["salReq"]) :
        print("içeri max")
        if cv["expectedValues"]["expectedSalary"] <= max_salary:
            mse+=0
        else:
            diff = cv["expectedValues"]["expectedSalary"] - max_salary
            if advert["salary"]["max"]<=0:
                err=diff/0.1
            else:
                err=diff/max_salary
            mse += (err ** 2) * weight
    else:
        print("elsemax")
        mse+=10000000
        
        
            
    

    # Beceriler karşılaştırması
    required_skills = set(advert["selectedSkills"]["skills"])
    candidate_skills = set(cv["skills"])
    common_skills = required_skills.intersection(candidate_skills)
    
        
    weight = advert["selectedSkills"]["skillScale"]
    total_weights += weight
        
    
    if advert["selectedSkills"]["skillReq"] and common_skills==required_skills:
        mse+=0
    elif (not advert["selectedSkills"]["skillReq"]) :
        if common_skills==required_skills:
            mse+=0
        else:
            if len(required_skills)<=0:
                err=1-(len(common_skills)/0.1)
            else:
                err=1-(len(common_skills)/len(required_skills))
            mse += (err**2) * weight
    else:
        mse+=10000000
        


    # Diller karşılaştırması
    required_languages=set(advert["selectedLanguages"]["languages"])
    candidate_languages = set(cv["languages"])
    common_languages = required_languages.intersection(candidate_languages)
    weight = advert["selectedLanguages"]["langScale"]
    total_weights += weight
    if advert["selectedLanguages"]["langReq"] and common_languages==required_languages:
        mse+=0
    elif (not advert["selectedLanguages"]["langReq"] ):
        if common_languages==required_languages:
            mse+=0
        else:
            if len(required_languages)<=0:
                err=1
            else:
                err=1-len(common_languages)/len(required_languages)
            mse += (err**2) * weight
    else:
        mse+=10000000
        
    

    # Şehir karşılaştırması
    required_cities=set(advert["locations"]["locations"])
    candidate_cities=set(cv["expectedValues"]["locations"])
    common_cities=required_cities.intersection(candidate_cities)

    if len(common_cities)<=0:
        mse+=100000000
    else:
        mse+=0
    
    # MSE'yi normalize edip başarı oranına dönüştür
    if total_weights > 0:
        mse /= total_weights
    
    success_rate = 1 - mse
    success_rate = round(success_rate, 2)
    return success_rate


# API'den veri çekme fonksiyonu
def fetch_data_from_api(url):
    response = requests.get(url)
    if response.status_code == 200:
        return response.json()
    else:
        raise Exception(f"API isteği başarısız oldu: {response.status_code}")

# CV ve ilanları eşleştirme
def match_cvs_with_adverts():
    threshold = 0.6

    print("cv önce")
    cvs = fetch_data_from_api('http://localhost:7190/cv')
    adverts = fetch_data_from_api('http://localhost:7190/advert')
    print("cv sonra")
    matched_cvs = []

    for advert in adverts:
        for cv in cvs:
            success_rate = calculate_success_rate(advert, cv)
            if success_rate >= threshold:
                matched_cvs.append({
                    "advertID": advert["id"],
                    "cvId": cv["id"],
                    "score": success_rate
                })

   
    output_dir = r'C:\Users\Furkan\Documents\kariyer-app-data'
    output_path = os.path.join(output_dir, 'matched_cv.json')
    
    with open(output_path, 'w', encoding='utf-8') as f:
        json.dump(matched_cvs, f, ensure_ascii=False, indent=4)

# İlanları CV'lerle eşleştirme
def match_adverts_with_cvs():
    threshold = 0.6

    print("cv önce")
    cvs = fetch_data_from_api('http://localhost:7190/cv')
    adverts = fetch_data_from_api('http://localhost:7190/advert')
    print("cv sonra")
    
    

    matched_adverts = []

    for cv in cvs:
        for advert in adverts:
            success_rate = calculate_success_rate(advert, cv)
            if success_rate >= threshold:
                matched_adverts.append({
                    "cvId": cv["id"],
                    "advertID": advert["id"],
                    "score": success_rate  
                })

    
    output_dir = r'C:\Users\Furkan\Documents\kariyer-app-data'
    output_path = os.path.join(output_dir, 'matched_adverts.json')

    with open(output_path, 'w', encoding='utf-8') as f:
        json.dump(matched_adverts, f, ensure_ascii=False, indent=4)

# Vue'den gelen post isteğini dinleyen Flask 
@app.route('/api/trigger', methods=['POST'])
def handle_trigger():
    try:
        
        match_cvs_with_adverts()
        match_adverts_with_cvs()

        
        notify_dotnet_api()

        return jsonify({"message": "Eşleştirme işlemi tamamlandı ve .NET API'ye sinyal gönderildi."}), 200
    except Exception as e:
        print(f"Error occurred: {e}")
        return jsonify({"error": str(e)}), 500

# .NET API'ye sinyal gönderme
def notify_dotnet_api():
    url = 'http://localhost:7190/advert/update'
    try:
        response = requests.get(url, verify=False)  
        response.raise_for_status()  
        print(response.json())  
    except requests.exceptions.RequestException as e:
        print(f"Error notifying .NET API: {e}")

if __name__ == '__main__':
    app.run(port=5000, debug=True)
