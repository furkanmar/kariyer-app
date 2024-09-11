using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace dotnet_api.Data
{
    public class Admin
    {
        private readonly string folderPath;
        private readonly string filePath, CVfilePath , UserFilePath, MatchesFilePath, MatchesAdvertFilePath;
        public List<Advert> adverts;
        public List<CV> Cvs;  
        public List<User> Users;
        public List<Matches> Matches;
        public List<Matches> MatchesAdverts;
        private int id;
        private int CVid;
        private int UserId;

        public Admin()
        {
            // Belgeler klasöründeki kariyer-app-data klasörüne erişim
            folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "kariyer-app-data");
            filePath = Path.Combine(folderPath, "adverts.json");
            CVfilePath = Path.Combine(folderPath, "cv.json");
            UserFilePath = Path.Combine(folderPath, "user.json");
            MatchesFilePath = Path.Combine(folderPath, "matched_cv.json");
            MatchesAdvertFilePath = Path.Combine(folderPath, "matched_adverts.json");


            // Klasörü oluştur
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Verileri yükle
            adverts = LoadAdvertsFromFile();
            Cvs=LoadCVFromFile();
            Users = LoadUserFromFile();
            Matches = LoadMatchesFromFile();
            MatchesAdverts = LoadMatchesadvertsFromFile();
            

            // Eğer veritabanında bir advert varsa id'yi ona göre başlatıyoruz
            id = adverts.Count > 0 ? adverts[^1].Id + 1 : 0;
            CVid = Cvs.Count > 0 ? Cvs[^1].Id + 1 : 0;
            UserId = Users.Count > 0 ? Users[^1].Id + 1 : 0;
        }

        public List<Matches> getMatchesCvById(int id)
        {
            return Matches.FindAll(a => a.advertID == id);
        }
        public List<Matches> getMatchesAdvertById(int id)
        {
            return Matches.FindAll(a => a.cvId == id);
        }

        public void Add(Advert advert)
        {
            advert.Id = id++;
            adverts.Add(advert);
            SaveAdvertsToFile();
        }
        public void CVAdd(CV cv)
        {
            cv.Id = CVid++;
            Cvs.Add(cv);
            SaveCVToFile();
        }
        public void AddUser(User user)
        {
            user.Id = UserId++;
            Users.Add(user);
            SaveUserToFile() ;
        }

        public void AdvertRemove(int id)

        {
            Matches.RemoveAll(a => a.advertID == id);
            SaveMatchesToFile();
            adverts.Remove(adverts.Find(a => a.Id == id));
            
            SaveAdvertsToFile();
        }
        public void UserRemove(User user)
        {
            if (user.Type == "Employer")
            {
                List<int> advertIdList = new List<int>();  
                for (int i = 0; i < adverts.Count; i++)
                {
                    if(user.Id == adverts[i].ActiveUser)
                    {
                        advertIdList.Add(adverts[i].Id);
                    }
                }
                for(int i=0;i < advertIdList.Count; i++)
                {
                    Matches.RemoveAll(a => a.advertID == advertIdList[i]);
                }
                
            }
            else
            {
                List<int> cvIdList = new List<int>();
                for (int i = 0; i < Cvs.Count; i++)
                {
                    if (user.Id == Cvs[i].ActiveUser)
                    {
                        cvIdList.Add(Cvs[i].Id);
                    }
                }
                for (int i = 0; i < cvIdList.Count; i++)
                {
                    Matches.RemoveAll(a => a.cvId == cvIdList[i]);
                }

            }
            Users.RemoveAll(a=> a.Id == user.Id);
            SaveUserToFile();
            SaveMatchesToFile();
        }

        public List<Advert> GetByID(int id)
        {
            return adverts.FindAll(a => a.ActiveUser== id);
        }
        public List<CV> GetCvsByID(int id)
        {
            return Cvs.FindAll(a => a.ActiveUser == id);
        }

        /*public List<Advert> GetByID(int id)
        {
            return adverts.FindAll(a => a.ActiveUser == id);
        }*/

        public List<Advert> getAllAdvert()
        {
            return adverts;
        }
        /*public Advert advertGetById(int id,List<Advert> list)
        {

            return list.Find(a => a.Id == id);
        }*/


        public User UserGetById(int id)
        {
            return Users.Find(a=>a.Id == id);
        }
        public void CVRemove(CV cv)
        {
            Cvs.RemoveAll(a => a.Id == cv.Id);
            SaveCVToFile();
        }

        public CV GetCVByID(int id)
        {
            return Cvs.Find(a => a.Id == id);
        }

        public void Update(Advert advert)
        {
            int index = adverts.FindIndex(a => a.Id == advert.Id);
            if (index != -1)
            {
                adverts[index] = advert;
                SaveAdvertsToFile();
            }
        }
        public void UserUpdate(User user)
        {
            int index = Users.FindIndex(a => a.Id == user.Id);
            if (index != -1)
            {
                Users[index] = user;
                SaveUserToFile();
            }
        }

        

        public Advert GetAdvert( int id)
        {
            return adverts.Find(a=> a.Id==id);
        }
        public List<User> GetUsers()
        {
            return Users;
        }
        public void CVUpdate(CV cv)
        {
            int index = Cvs.FindIndex(a => a.Id == cv.Id);
            if (index != -1)
            {
                Cvs[index] = cv;
                SaveCVToFile();
            }
        }

        public List<CV> GetCV()
        {
            return Cvs;
        }


        private void SaveAdvertsToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(adverts, options);
            File.WriteAllText(filePath, json);
        }

        
        
        private void SaveCVToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Cvs, options);
            File.WriteAllText(CVfilePath, json);
        }
        private void SaveUserToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Users, options);
            File.WriteAllText(UserFilePath, json);
        }
        private void SaveMatchesToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Matches, options);
            File.WriteAllText(MatchesFilePath, json);
        }
        private List<Advert> LoadAdvertsFromFile()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Advert>>(json) ?? new List<Advert>();
            }
            return new List<Advert>();
        }


        private List<CV> LoadCVFromFile()
        {
            if (File.Exists(CVfilePath))
            {
                string json = File.ReadAllText(CVfilePath);
                return JsonSerializer.Deserialize<List<CV>>(json) ?? new List<CV>();
            }
            return new List<CV>();
        }
        private List<User> LoadUserFromFile()
        {
            if (File.Exists(UserFilePath))
            {
                string json = File.ReadAllText(UserFilePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            return new List<User>();
        }
        private List<Matches> LoadMatchesFromFile()
        {
            if (File.Exists(MatchesFilePath))
            {
                string json = File.ReadAllText(MatchesFilePath);
                return JsonSerializer.Deserialize<List<Matches>>(json) ?? new List<Matches>();
            }
            return new List<Matches>();
        }
        private List<Matches> LoadMatchesadvertsFromFile()
        {
            if (File.Exists(MatchesAdvertFilePath))
            {
                string json = File.ReadAllText(MatchesAdvertFilePath);
                return JsonSerializer.Deserialize<List<Matches>>(json) ?? new List<Matches>();
            }
            return new List<Matches>();
        }
        public void ReloadData()
        {
            adverts = LoadAdvertsFromFile();
            Cvs = LoadCVFromFile();
            Matches = LoadMatchesFromFile();
        }
    }
}
