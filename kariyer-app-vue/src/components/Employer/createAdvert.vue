<template>
    
    <div class="container mt-5">
      <form @submit.prevent="submitForm">
        <div class="row">
          <label class="form-label section-title"> Genel Bilgiler</label>  
          
          <!-- İş Başlığı -->
          <div class="mb-3">
            <label for="jobTitle" class="form-label section-title">İş Başlığı</label>
            <input id="jobTitle" type="text" class="form-control" v-model="jobTitle" />
          </div>
    
          <!-- Şirket İsmi -->
          <div class="mb-3">
            <label for="companyName" class="form-label section-title">Şirket İsmi</label>
            <input id="companyName" type="text" class="form-control" v-model="companyName" />

          </div>
    
          <!-- Çalışma Şekli -->
          <div class="mb-3">
            <label for="workType" class="form-label section-title">Çalışma Şekli</label>
            <select id="workType" class="form-select" v-model="workType">
              <option value="" disabled selected>Seçiniz</option>
              <option value="Tam Zamanlı">Tam Zamanlı</option>
              <option value="Yarı Zamanlı">Yarı Zamanlı</option>
              <option value="remote">Uzaktan</option>
              <option value="Staj">Staj</option>
            </select>
          </div>


          <!--Aranan Pozisyon-->
          <div class="mb-3">
            <label for="position" class="form-label section-title">Aranan Pozisyon</label>
            <input id="position" type="text" class="form-control" v-model="position" />
          </div>
      </div>

        <!--Şehir-->
        
        <div class="mb-3 row">
            <label for="cityInput" class="form-label section-title" >Şehir Seçin</label>
            <input
            class="form-control"
            list="cityOptions"
            id="cityInput"
            placeholder="Şehir adı yazın..."
            v-model="location"
            @change="addCity(location)"
            :disabled="workType === 'remote'"
            />
            <datalist id="cityOptions">
            <option v-for="city in filteredCities" :key="city" :value="city" >{{ city }}</option>
            </datalist>
          <div class="btn-group col-md-3 mb-2" role="group" aria-label="Basic example"
              v-for="(location, index) in locations.locations" :key="index"
              @click.prevent="deleteLocation(index)">
              <button 
              type="button" class="btn btn-success "
              >{{ location }}</button>

          </div>
        
          <div class="mb-3">
            <div class="col-md-4 mt-3">
              <input class="form-check-input " type="checkbox" name="inlineRadioOptions" id="locReq" v-model="locations.locReq">
              <label class="form-check-label" for="locReq">Zorunlu mu?</label>
          </div>

          </div>

              
        </div>
        

        <!-- Eğitim Seviyesi -->
        <div class="mb-3 row">
          <label for="educationLevel" class="form-label section-title">Eğitim Seviyesi</label>
          <div class="col-md-8">
           
            <select id="educationLevel" class="form-select" v-model="educationLevel.Level">
              <option value="" disabled selected>Seçiniz</option>
              <option value="Lise">Lise</option>
              <option value="Önlisans">Önlisans</option>
              <option value="Lisans">Lisans</option>
              <option value="Yüksek Lisans">Yüksek Lisans</option>
              <option value="Doktora">Doktora</option>
            </select>
          </div>
          <div class="col-md-4 mt-2">
              <input class="form-check-input" type="checkbox" name="inlineRadioOptions" id="eduReq" v-model="educationLevel.eduReq">
              <label class="form-check-label" for="EduReq">Zorunlu mu?</label>
          </div>
        </div>
        
        <!-- Çalışma Saatleri -->
        <div class="mb-3 row">
          <label for="workingHours" class="form-label section-title">Çalışma Saatleri</label>
          <div class="col-md-8">
            
            <select id="workingHours" class="form-select" v-model="workingHours.Hour">
              <option value="" disabled selected>Seçiniz</option>
              <option value="09:00 - 17:00">09:00 - 17:00</option>
              <option value="10:00 - 18:00">10:00 - 18:00</option>
              <option value="Esnek Saatler">Esnek Saatler</option>
            </select>
          </div>
          <div class="col-md-4 mt-2">
              <input class="form-check-input" type="checkbox" name="inlineRadioOptions" id="hourReq" v-model="workingHours.HourReq">
              <label class="form-check-label" for="hourReq">Zorunlu mu?</label>
          </div>
        </div>

        <!-- Deneyim -->
        <div class=" row">
          <label for="experience" class="form-label section-title">Deneyim(Yıl)</label>
          <div class="col-md-6  ">
            <input id="experience" type="number" class="form-control" v-model="experience.years" />
          </div>
          <div class="col-md-2 mt-2">
            <input class="form-check-input" type="checkbox" name="inlineRadioOptions" id="XPReq" value="true" v-model="experience.xpReq">
            <label class="form-check-label" for="XPReq">Zorunlu mu?</label>
          </div>
          <div class="col-md-4">
            <form>
              <label for="customRange" class="form-label">Önem Derecesini Belirtiniz?(1-5)</label>
              <div class="d-flex align-items-center" >
                <input
                  type="range"
                  class="form-range"
                  id="customRange"
                  min="1"
                  max="5"
                  step="1"
                  v-model="experience.xpScale"
                  :disabled="experience.xpReq"
                  
                />
                <span class="range-value">{{ experience.xpScale }}</span>
              </div>
            </form>

          </div>

        </div>
  
        <!-- Şirket Çalışan Sayısı -->
         <div class="row">
          <label for="companySize" class="form-label section-title">Şirket Çalışan Sayısı</label>
          <div class="col-md-6">
            
            <input id="companySize" type="number" class="form-control" v-model="companySize.Size" />
          </div>
            <div class="col-md-2 mt-2">
              <input class="form-check-input" type="checkbox" name="inlineRadioOptions" id="compReq" value="true" v-model="companySize.compReq">
              <label class="form-check-label" for="compReq">Zorunlu mu?</label>
            </div>
            <div class="col-md-4">
              <form>
                <label for="customRange3" class="form-label">Önem Derecesini Belirtiniz?(1-5)</label>
                <div class="d-flex align-items-center">
                  <input
                    type="range"
                    class="form-range"
                    id="customRange3"
                    min="1"
                    max="5"
                    step="1"
                    v-model="companySize.compScale"
                    :disabled="companySize.compReq"
                  />
                  <span class="range-value">{{ companySize.compScale }}</span>
                </div>
              </form>

            </div>
          
        </div>
  
        <!-- Maaş Aralığı -->
        <div class="mb-3">
          
          <div class="row">
            <label for="salary" class="form-label section-title">Maaş Aralığı</label>
            
            <form class="col-md-3 form-floating">
              <input id="salaryMin" type="number" class="form-control" v-model="salary.Min" placeholder="Maaş Alt Limit" required/>
              <label for="salaryMin">Alt Limit</label>
            </form>
            <form class="col-md-3 form-floating" >
              <input id="salaryMax" type="number" class="form-control" v-model="salary.Max" placeholder="Maaş Üst limit" required/>
              <label for="salaryMax">Üst limit</label>
            </form>
            <div class="col-md-2 mt-3">
              <input class="form-check-input" type="checkbox" name="inlineRadioOptions" id="salReq" value="true" v-model="salary.salReq">
              <label class="form-check-label" for="salReq">Zorunlu mu?</label>
            </div>
            <div class="col-md-4">
              <form>
                <label for="customRange2" class="form-label">Önem Derecesini Belirtiniz?(1-5)</label>
                <div class="d-flex align-items-center">
                  <input
                    type="range"
                    class="form-range"
                    id="customRange2"
                    min="1"
                    max="5"
                    step="1"
                    v-model="salary.salScale"
                    :disabled="salary.salReq"
                  />
                  <span class="range-value">{{ salary.salScale }}</span>
                </div>
              </form>

            </div>

          </div>
          
          
          
            
          
        </div>
  
        
  
        
  
        <!-- Beceri Seti -->
        <div class="mb-3 row">
          <label for="skills" class="form-label section-title">Yetenekler</label> 
          <div class="input-group-preppend ">
            
            <span class="input-group-addon"></span>
            <input
            class="form-control"
            id="SkillInput"
            placeholder="Yeteneği yazınız.."
            v-model="searchVal"
            />
          </div>
          <div class="btn-group form-container ">
            <button
              v-for="skill in filteredSkills"
              :key="skill"
              type="button"
              class="btn flex-item"
              :class="{'btn-outline-success': !selectedSkills.skills.includes(skill), 'btn-success': selectedSkills.skills.includes(skill)}"
              @click="toggleSkill(skill)"
            >
              {{ skill }}
            </button>
          </div>
          <div class="btn-group col-md-3 mb-2" role="group" aria-label="Basic example"
          v-for="(skill, index) in selectedSkills.skills" :key="index"
          @click="deleteSkill(index)"
          >
            <button type="button" class="btn btn-success"
            >{{ skill }}</button>     
          </div>
        </div>
        
        <div class="row">
          <div class="col-md-2 mt-2">
              <input class="form-check-input" type="checkbox" name="inlineRadioOptions" id="skillReq" value="true" v-model="selectedSkills.skillReq">
              <label class="form-check-label" for="skillReq">Zorunlu mu?</label>
          </div>
          <div class="col-md-4">
            <form>
              <label for="customRange4" class="form-label">Önem Derecesini Belirtiniz?(1-5)</label>
            
              
              <div class="d-flex align-items-center">
                <input
                  type="range"
                  class="form-range"
                  id="customRange4"
                  min="1"
                  max="5"
                  step="1"
                  v-model="selectedSkills.skillScale"
                  
                />
                <span class="range-value">{{ selectedSkills.skillScale }}</span>
              </div>
            </form>

          </div>

        </div>
  
        
  
        
  
        <!-- Konuştuğu Diller -->
        <div class="mb-3 row">
          <label for="languages" class="form-label section-title">Konuştuğu Diller</label>
          <div class="btn-group flex-container">
            <button
              v-for="language in languagesList"
              :key="language"
              type="button"
              class="btn flex-item"
              :class="{'btn-outline-success': !selectedLanguages.Languages.includes(language), 'btn-primary': selectedLanguages.Languages.includes(language)}"
              @click="toggleLanguage(language)"
            >
              {{ language }}
            </button>
          </div>
        </div>
        <div class="row">
          <div class="col-md-2 mt-2">
              <input class="form-check-input" type="checkbox" name="inlineRadioOptions" id="langReq" value="true" v-model="selectedLanguages.langReq">
              <label class="form-check-label" for="langReq">Zorunlu mu?</label>
          </div>
          <div class="col-md-4">
            <form>
              <label for="customRange5" class="form-label">Önem Derecesini Belirtiniz?(1-5)</label>
            
              
              <div class="d-flex align-items-center">
                <input
                  type="range"
                  class="form-range"
                  id="customRange5"
                  min="1"
                  max="5"
                  step="1"
                  v-model="selectedLanguages.langScale"
                  
                />
                <span class="range-value">{{ selectedLanguages.langScale }}</span>
              </div>
            </form>

          </div>

        </div>



        <!-- Sertifikalar -->
        <div class="mb-3 row">
          <label for="certifications" class="form-label section-title">Sertifikalar</label>
          <input id="certifications" type="text" class="form-control" v-model="certifications" />
        </div>
        <!-- Açıklama -->
        <div class="mb-3 row">
          <label for="description" class="form-label section-title  ">İş Açıklaması</label>
          <textarea id="description" class="form-control" rows="3" v-model="description"></textarea>
        </div>

        <!--Yayınlama ve kaydet butonu-->
        <div class="mb-3">        
          <div class="form-check">

            <input id="published" type="checkbox" class="form-check-input" v-model="isPublished" />
            <label for="published" class="form-check-label">Yayınla</label>
          </div>
        </div>
  
        <!-- Kaydet Butonu -->
        <button type="submit" class="btn btn-dark">Kaydet</button>
      </form>
    </div>
  </template>

  
  <script>

import axios from 'axios';

  
  export default {
    
    props:{
       id: {
      type: Number,
      required: false
    },
      editMode: {
      type: Boolean,
      required: true
    }
    },
    
    data() {
      return {
        Id:0,
        
        jobTitle: '',
        companyName: '',
        workType: '',
        experience: {years:0,xpReq:false,xpScale:3},
        companySize: {Size:0,compReq:false,compScale:3},
        salary: {Min:null,Max:null, salReq:false,salScale:3},
        description: '',
        educationLevel: {Level:'',eduReq:false},
        selectedSkills: {skills:[],skillReq:false,skillScale:3},
        selectedLanguages:{Languages:[],langReq:false,langScale:3},
        workingHours: {Hour:'',HourReq:false},
        certification: '',
        location: '',
        locations: {locations:[],locReq:false},
        position: '',
        isPublished: true,


        
        mode:this.editMode,
        
        
        cities: [],
        searchVal: '',
        skillsList: [],
        languagesList: ['İngilizce', 'Almanca', 'Fransızca', 'İspanyolca', 'İtalyanca', 'Çince'],
       
        advertIndex: null,
        advertList: [],
        successMessage: '',
        ActiveUserId:0,
        ActiveUserType:'',
        
        
      };
    },
    computed: {
      filteredCities() {
        const query = this.location.toLowerCase();
        return this.cities.filter(city =>
          city.toLowerCase().includes(query)
        );
      },
      filteredSkills() {
        return this.skillsList.filter(skill =>
          skill.toLowerCase().includes(this.searchVal.toLowerCase())
        );
      }
    },
    mounted() {
      this.ActiveUserId=localStorage.getItem("ActiveUserId");
      this.ActiveUserType=localStorage.getItem("ActiveUserType");
      
    console.log('Active User ID:', this.ActiveUserId);
    console.log('Active User Type:', this.ActiveUserType);
  
      
      
    
      this.loadSkills();
      this.loadCity();
    },
    created() {
      
      if (this.mode) {
        console.log(this.id);
        axios.get(`http://localhost:7190/get/${this.id}`)
          .then(res => {
            const advert = JSON.parse(JSON.stringify(res.data));
            console.log(advert);
            this.loadAdvert(advert);
            
          }
        )
          .catch(err => {
            console.error('Error fetching advert for editing:', err);
          });
      }
    },
    
    methods: {
      notifyPython() {
        axios.post('http://localhost:5000/api/trigger')
          .then(() => {
            console.log("Python servisi tetiklendi.");
          })
          .catch(error => {
            console.error("Python tetikleme hatası:", error);
          });
      },
      
      loadAdvert(advert) {
        if (advert != null) {
            this.Id = advert.id;
            this.ActiveUser = advert.ActiveUser;
            this.jobTitle = advert.jobTitle;
            this.companyName = advert.companyName;
            this.workType = advert.workType;
            
            // Experience nesnesini güncelleme
            this.experience.years = advert.experience.years;
            this.experience.xpReq = advert.experience.xpReq;
            this.experience.xpScale = advert.experience.xpScale;
            
            // CompanySize nesnesini güncelleme
            this.companySize.Size = advert.companySize.size;  
            this.companySize.compReq = advert.companySize.compReq;
            this.companySize.compScale = advert.companySize.compScale;
            
            // Salary nesnesini güncelleme
            this.salary.Min = advert.salary.min;  // 'min' küçük harf ile başlıyor
            this.salary.Max = advert.salary.max;  // 'max' küçük harf ile başlıyor
            this.salary.salReq = advert.salary.salReq;
            this.salary.salScale = advert.salary.salScale;

            this.description = advert.description;
            
            // EducationLevel nesnesini güncelleme
            this.educationLevel.Level = advert.educationLevel.level;  // 'level' küçük harf ile başlıyor
            this.educationLevel.eduReq = advert.educationLevel.eduReq;
            
            // SelectedSkills nesnesini güncelleme
            this.selectedSkills.skills = advert.selectedSkills.skills || [];
            this.selectedSkills.skillReq = advert.selectedSkills.skillReq;
            this.selectedSkills.skillScale = advert.selectedSkills.skillScale;

            // WorkingHours nesnesini güncelleme
            this.workingHours.Hour = advert.workingHours.hour;  // 'hour' küçük harf ile başlıyor
            this.workingHours.HourReq = advert.workingHours.hourReq;

            this.certification = advert.certification;  // 'certification' küçük harf ile başlıyor

            // SelectedLanguages nesnesini güncelleme
            this.selectedLanguages.Languages = advert.selectedLanguages.languages || []; // 'languages' küçük harf ile başlıyor
            this.selectedLanguages.langReq = advert.selectedLanguages.langReq;
            this.selectedLanguages.langScale = advert.selectedLanguages.langScale;

            this.isPublished = advert.isPublished;

            // Locations nesnesini güncelleme
            this.locations.locations = advert.locations.locations || []; 
            this.locations.locReq = advert.locations.locReq;

            this.position = advert.position;
            this.isPublished=advert.isPublished;
        } else {
            console.log("advert is null");
        }
      },

        


      deleteSkill(index) {
        this.selectedSkills.skills.splice(index, 1);
      },
      addCity(city) {
        if (city && !this.locations.locations.includes(city)) {
          this.locations.locations.push(city);
          this.location = '';
        }
      },
      async loadCity() {
        try {
          const response = await fetch('/city.json');
          if (!response.ok) {
            throw new Error('Ağ hatası: ' + response.statusText);
          }
          const data = await response.json();
          this.cities = data;
        } catch (error) {
          console.error('Şehirler yüklenirken bir hata oluştu:', error);
        }
      },
      deleteLocation(index) {
        this.locations.locations.splice(index, 1);
      },
      async loadSkills() {
        try {
          const response = await fetch('/skills.json');
          if (!response.ok) {
            throw new Error('Ağ hatası: ' + response.statusText);
          }
          const data = await response.json();
          this.skillsList = data;
        } catch (error) {
          console.error('Beceriler yüklenirken bir hata oluştu:', error);
        }
      },
      toggleSkill(skill) {
        const index = this.selectedSkills.skills.indexOf(skill);
        if (index === -1) {
          this.selectedSkills.skills.push(skill);
        } else {
          this.selectedSkills.skills.splice(index, 1);
        }
      },
      toggleLanguage(language) {
        const index = this.selectedLanguages.Languages.indexOf(language);
        if (index === -1) {
          this.selectedLanguages.Languages.push(language);
        } else {
          this.selectedLanguages.Languages.splice(index, 1);
        } 
      },
      submitForm() {
        console.log(this.ActiveUserId);

        const advert = {
            Id: parseInt(this.Id),
            ActiveUser: parseInt(this.ActiveUserId),
            jobTitle: this.jobTitle || '',
            companyName: this.companyName || '',
            workType: this.workType || '',
            
            experience: {
                years: this.experience?.years ? parseInt(this.experience.years) : 0,
                xpReq: this.experience?.xpReq || false,
                xpScale: this.experience?.xpScale ? parseInt(this.experience.xpScale) : 3
            },
            
            companySize: {
                Size: this.companySize?.Size ? parseInt(this.companySize.Size) : 0,
                compReq: this.companySize?.compReq || false,
                compScale: this.companySize?.compScale ? parseInt(this.companySize.compScale) : 3
            },
            
            salary: {
                Min: this.salary?.Min ? parseFloat(this.salary.Min) : null,
                Max: this.salary?.Max ? parseFloat(this.salary.Max) : null,
                salReq: this.salary?.salReq || false,
                salScale: this.salary?.salScale ? parseInt(this.salary.salScale) : 3
            },
            
            description: this.description || '',
            
            educationLevel: {
                Level: this.educationLevel?.Level || '',
                eduReq: this.educationLevel?.eduReq || false
            },
            
            selectedSkills: {
                skills: this.selectedSkills?.skills || [],
                skillReq: this.selectedSkills?.skillReq || false,
                skillScale: this.selectedSkills?.skillScale ? parseInt(this.selectedSkills.skillScale) : 3
            },
            
            selectedLanguages: {
                Languages: this.selectedLanguages?.Languages || [],
                langReq: this.selectedLanguages?.langReq || false,
                langScale: this.selectedLanguages?.langScale ? parseInt(this.selectedLanguages.langScale) : 3
            },
            
            workingHours: {
                Hour: this.workingHours?.Hour || '',
                HourReq: this.workingHours?.HourReq || false
            },
            
            certification: this.certification || '',
            location: this.location || '',
            
            locations: {
                locations: this.locations?.locations || [],
                locReq: this.locations?.locReq || false
            },
            
            position: this.position || '',
            isPublished:this.isPublished||false
      };
      console.log(advert);

      if (this.id) {
        // Düzenleme modu: PUT isteği
        
        axios.put(`http://localhost:7190/advert/`, advert)
          .then(() => {
            this.notifyPython();
            this.$router.push('/adverts');
          })
          .catch(err => {
            console.error('Error updating advert:', err);
          });
      } else {
        
        // Yeni ilan oluşturma modu: POST isteği
        axios.post('http://localhost:7190/advert', advert)
          .then(() => {
            this.notifyPython();
            this.$router.push('/adverts'  );
          })
          .catch(err => {
            console.error('Error creating advert:', err);
          });
      }
    }
    }
};
  </script>
  
  <style>
  
  .flex-container {
    display: flex;
    flex-wrap: wrap;
    gap: 5px;
  }
  
  .flex-item {
    flex-basis: calc(10% - 10px);
    text-align: center;
  }
  .range-value {
    font-size: 1.2rem;
    font-weight: bold;
    margin-left: 10px;
  }

  </style>
  