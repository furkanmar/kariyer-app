<template>
  
    <div class="container mt-5">
      
        <!-- Kişisel Bilgiler -->
        <div class="mb-3 row">
          <label for="name" class="form-label section-title">Kişisel bilgiler</label>
          <div class="input-group">
            <input id="firstname" type="text" class="form-control" v-model="PersonalInfo.FirstName" placeholder="İsim" />
            <span class="input-group-addon"></span>
            <input id="lastname" type="text" class="form-control" v-model="PersonalInfo.LastName" placeholder="Soy isim" />
            <span class="input-group-addon"></span>
            <input id="age" type="text" class="form-control" v-model="PersonalInfo.Age"  placeholder="Yaş"/>
            <span class="input-group-addon"></span>
            <select id="gender" class="form-select"  v-model="PersonalInfo.Gender"  >
                <option value="" disabled selected>Cinsiyet</option>
                <option value="Male">Erkek</option>
                <option value="Female">Kadın</option>
                <option value="notMention">Belirmek İstemiyorum</option>
            </select>
          </div>
          <div >
            <textarea name="adress" id="adress" 
            class="form-control mt-1"
            placeholder="Adres"
            v-model="PersonalInfo.Address"></textarea>
          </div>
        </div>

        <!--İletişim Bilgileri-->
        <div class="mb-3 row">
          <label for="contactInfo" class="form-label section-title">İletişim Bilgileri</label>
          <div class="input-group">
            <input id="email" type="text" class="form-control" v-model="ContactInfo.Email" placeholder="E-mail" />
            <span class="input-group-addon"></span>
            <input id="phoneNumber" type="text" class="form-control" v-model="ContactInfo.Phone" placeholder="Telefon Numarası" />
            </div>
        </div>

        <!-- Eğitim Bilgileri-->
        <div class="mb-3">
          <label for="eduInfo" class="form-label section-title ">Eğitim Bilgileri</label>
          <div class="input-group">
            <select id="eduInfo" class="form-select" v-model="EducationInfo.EduLevel" >
                <option value="" disabled selected>Eğitim Seviyesi</option>
                <option v-for="edu in educationLevel"
                :key="edu"
                :value="edu">{{ edu }}</option>
            </select>

            <span class="input-group-addon"></span>

            <select id="eduInfo" class="form-select"  v-model="EducationInfo.Status"  >
                <option value="" disabled selected>Öğrenim Durumu</option>
                
                <option value="graduated">Mezun</option>
                <option value="student">Öğrenci</option>
            </select>
            </div>
        </div>
        
        <!--Dil seçimi-->
        <div class="mb-3">
          <label for="languages" class="section-title">Konuştuğu Diller</label>
          <div class="btn-group flex-container">
            <button
              v-for="language in languagesList"
              :key="language"
              type="button"
              class="btn flex-item"
              :class="{'btn-outline-success': !SelectedLanguages.includes(language), 'btn-success': SelectedLanguages.includes(language)}"
              @click="toggleLanguage(language)"
            >
              {{ language }}
            </button>
          </div>
        </div>

        <!-- Çalışma şartları beklentisi-->
        <div class="row">
          <label  class="form-label section-title">Çalışma Şartları Beklentileriniz</label>
          <div class="mb-1">
              <label for="cityInput" class="form-label section-title" >Çalışabileceğiniz Şehirleri Seçiniz</label>
              <input
              class="form-control"
              list="cityOptions"
              id="cityInput"
              placeholder="Şehir adı yazın..."
              v-model="location"
              @change="addCity(location)"
              
              />
              <datalist id="cityOptions">
              <option v-for="city in filteredCities" :key="city" :value="city" >{{ city }}</option>
              </datalist>
            <div class="btn-group col-md-3 mb-2" role="group" aria-label="Basic example"
                v-for="(location, index) in ExpectedValues.locations" :key="index"
                @click.prevent="deleteLocation(index)">
                <button 
                type="button" class="btn btn-success "
                >{{ location }}</button>

            </div>
          </div>
        </div>
        <div class="row">
          <div class="mb-1">
            <label for="expedtedSalary" class="form-label section-title">Maaş beklentiniz</label>
            <input type="number" 
            class="form-control"
            id="expectedSalary"
            v-model="ExpectedValues.expectedSalary">
          </div>
        </div>
        <div class="row">
          
          <div class="mb-1">
            <label for="workingHours" class="form-label section-title">Çalışma Saatleri</label>
            <select id="workingHours" class="form-select" v-model="ExpectedValues.expectedWorkHours">
              <option value="" disabled selected>Seçiniz</option>
              <option value="09:00 - 17:00">09:00 - 17:00</option>
              <option value="10:00 - 18:00">10:00 - 18:00</option>
              <option value="Esnek Saatler">Esnek Saatler</option>
            </select>
          </div>
        </div>

          

        <!--İş Deneyimi-->
        <div class="mb-3 row">
          <label for="XP" class="section-title form-label">İş Deneyimleri</label>
          <div class="mb-3">
            
            <input type="text" class="form-control mb-2" v-model="Company.CompanyName" placeholder="Şirket İsmi">
            <input type="text" class="form-control mb-2" v-model="Company.CompanyField" placeholder="Çalıştığı Sektör">
            <input type="number" class="form-control mb-2" v-model="Company.CompanySize" placeholder="Çalışan Sayısı">
            <input type="number" class="form-control" v-model="Company.Years" placeholder="Ne kadar çalıştınız?">
            
          </div>   
          <div class="d-inline  mb-2 mt-1">
            <button class=" btn btn-warning" style="float:right" @click.prevent="handleCompany">Ekle</button> 
            <div class="btn-group " role="group" aria-label="Basic example"
            v-for="(company, index) in Companies" :key="index"
            @click="deleteXP(index)"
            >
              <button type="button" class="btn btn-success text-uppercase"
              >{{ company.CompanyName }}</button>
              
            </div>
          </div>
          
        </div>

        <!--Bir önceki İş-->
        <div class="mt-2">
          <label for="preJob" class="section-title">En Sonki İşiniz </label>
          <div class="mb-3">
            <input type="text" class="form-control mb-2 " v-model="PreviousJob.Name" placeholder="Şirket İsmi">
            <input type="text" class="form-control mb-2 " v-model="PreviousJob.Position" placeholder="Poziyonunuz"> 

          </div>
        </div>

        

        <!--Yetenekler-->
        <div class="mb-3">
          <div class="input-group-preppend ">
            <label for="skills" class="section-title">Yetenekler</label>
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
              :class="{'btn-outline-success': !SelectedSkills.includes(skill), 'btn-success': SelectedSkills.includes(skill)}"
              @click="toggleSkill(skill)"
            >
              {{ skill }}
            </button>
          </div>
          <div class="btn-group" role="group" aria-label="Basic example"
          v-for="(skill, index) in SelectedSkills" :key="index"
          @click="deleteSkill(index)"
          >
            <button type="button" class="btn btn-success"
            >{{ skill }}</button>
            
          </div>
        </div>

        <!--Projeler-->
        <div >
          <label for="project" class="section-title">Projeler</label>
          <div class="mb-3">
            
            <input type="text" class="form-control mb-2" v-model="Project.Title" placeholder="Projenin Başlığı">
            <input type="text" class="form-control mb-2" v-model="Project.Content" placeholder="Projenin Açıklaması">
            <input type="number" class="form-control" v-model="Project.Time" placeholder="Ne kadar çalıştınız?">
             
          </div>  
          <label for="" class="form-label" style="border-bottom: 1px solid #333;">Proje ile ilgili olan yeteneklerinizi seçiniz</label>     
          <div class="btn-group form-container ">
            <button
              v-for="skill in SelectedSkills"
              :key="skill"
              type="button"
              class="btn flex-item"
              :class="{'btn-outline-success': !FilteredSelectedSkill.includes(skill), 'btn-success': FilteredSelectedSkill.includes(skill)}"
              @click="toggleFilteredSkill(skill)"
            >
              {{ skill }}
            </button>
            
          </div>
          <div class="d-inline-block mb-2 mt-1">
            <button class=" btn btn-warning "  @click.prevent="handleProject">Ekle</button>
            <div class="btn-group " role="group" aria-label="Basic example"
            v-for="(project, index) in Projects" :key="index"
            @click="deleteProject(index)"
            >
              <button type="button" class="btn btn-success text-uppercase "
              >{{ project.Title }}</button>
            </div>
            
          </div>
          
        </div>





        <!--Hobiler-->
        <div class="mb-3 ">
          <label for="preJob" class="section-title">Hobileriniz </label>
          <div class="input-group">
            <input type="text" class="form-control mb-2" v-model="Interest" placeholder="Hobileriniz">
            <span class="input-group-addon"></span>
            <button class=" btn btn-warning mb-2"  @click.prevent="handleHobi(Interest)">Ekle</button> 
            
          </div>
          
          <div class="btn-group" role="group" aria-label="Basic example"
          v-for="(interest, index) in Interests" :key="index"
          @click="deleteInterest(index)"
          >
            <button type="button" class="btn btn-success"
            >{{ interest }}</button>
            
          </div>
        </div>

        <!-- Özgeçmiş Yükleme -->
        <div>
          <label for="CV" class="section-title" 
          
          >Özgeçmiş Yükleme</label>
          <div class="mb-3 ">
            <input id="resume" type="file" class="form-control" @change="handleFileUpload" />
          </div>
        </div>


        


        <!--Submit Buttonu-->
        <div class="d-flex flex-row-reverse">
            <div class="p-2">
                <button type="submit"
                 class="btn btn-primary"
                 @click.prevent="submitForm" >Kaydet</button>
            </div>
        
        </div>
        
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
        
        PersonalInfo:{FirstName:'',LastName:'',Age:'',Gender:'',Address:''},
        ContactInfo:{Email:'',Phone:''},
        EducationInfo: {EduLevel:'',Status:''},

        Company:{CompanyName:'',CompanyField:'',CompanySize:0,Years:0},
        Companies:[],

        PreviousJob:{Name:'',Position:''},

        SelectedSkills: [],

        Interests:[],
        Interest:'',
        ExpectedValues:{locations:[],expectedSalary:0,expectedWorkHours:''},

        SelectedLanguages: [],

        Project: {Title:'',Content:'',Time:0,RelatedSkills:[]},
        Projects:[],
        FilteredSelectedSkill:[],
        

        Resume: "" ,

        searchVal:'',
        deleteIndex:'',
        
  
   
        
        location:'',
        cities: [],
        educationLevel:['Lise','Önlisans','Lisan','Yüksek Lisans','Doktora'],
        skillsList: [],
        languagesList: ['İngilizce', 'Almanca', 'Fransızca', 'İspanyolca', 'İtalyanca', 'Çince'],
           
        mode:this.editMode,
      }
      
    },
    
    computed: {
        filteredSkills() {
          // Arama girdisine göre skillsList'i filtrele
          return this.skillsList.filter(skill =>
            skill.toLowerCase().includes(this.searchVal.toLowerCase())
          );
        },
        filteredCities() {
          const query = this.location.toLowerCase();
          return this.cities.filter(city =>
            city.toLowerCase().includes(query)
          );
        },
    },
    mounted(){
      this.loadSkills();
      this.loadCity();
      
    },
    created(){
      this.ActiveUser=localStorage.getItem("ActiveUserId");
      if (this.mode) {
        console.log(this.id);
        axios.get(`http://localhost:7190/cv/${this.id}`)
          .then(res => {
            const cv = JSON.parse(JSON.stringify(res.data));
            console.log(cv);
            this.loadCv(cv);
            
          }
        )
          .catch(err => {
            console.error('Error fetching advert for editing:', err);
          });
      }
      
    },
    
    methods:{
      loadCv(cv) {
        this.Id = cv.id;
        
        this.PersonalInfo = {
            FirstName: cv.personalInfo.firstName || '',
            LastName: cv.personalInfo.lastName || '',
            Age: cv.personalInfo.age || '',
            Gender: cv.personalInfo.gender || '',
            Address: cv.personalInfo.address || ''
        };
        
        this.ContactInfo = {
            Email: cv.contactInfo.email || '',
            Phone: cv.contactInfo.phone || ''
        };
        
        this.EducationInfo = {
            EduLevel: cv.educationInfo.eduLevel || '',
            Status: cv.educationInfo.status || ''
        };
        
        this.Companies = cv.companies.map(company => ({
            CompanyName: company.companyName || '',
            CompanyField: company.companyField || '',
            CompanySize: company.companySize || 0,
            Years: company.years || 0
        }));

        this.PreviousJob = {
            Name: cv.previousJob.name || '',
            Position: cv.previousJob.position || ''
        };
        
        this.ExpectedValues = {
            locations: cv.expectedValues.locations || [],
            expectedSalary: cv.expectedValues.expectedSalary || 0,
            expectedWorkHours: cv.expectedValues.expectedWorkHours || ''
        };

        this.SelectedSkills = cv.skills || [];
        this.Interests = cv.interests || [];
        this.SelectedLanguages = cv.languages || [];
        
        this.Projects = cv.projects.map(project => ({
            Title: project.title || '',
            Content: project.content || '',
            Time: project.time || 0,
            RelatedSkills: project.relatedSkills || []
        }));

        this.Resume = cv.resume || '';
    },

      
    

      addCity(city) {
        if (city && !this.ExpectedValues.locations.includes(city)) {
          this.ExpectedValues.locations.push(city);
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
        this.ExpectedValues.locations.splice(index, 1);
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
      async loadSkills() {
        try {
          // const response = await fetch('/skills.json');
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
      deleteSkill(index){
        this.SelectedSkills.splice(index, 1);
      },
      deleteInterest(index){
        this.Interests.splice(index, 1);
      },
      deleteProject(index){
        this.Projects.splice(index, 1);
      },
      deleteXP(index){
        this.Companies.splice(index, 1);
      },
      handleCompany(){
        const Company={
          CompanyName:this.Company.CompanyName,
          CompanyField:this.Company.CompanyField,
          Years:parseInt(this.Company.Years),
          CompanySize:parseInt(this.Company.CompanySize),
        };
        this.Companies.push(Company);
        
        this.Company.CompanyName = '';
        this.Company.CompanyField = '';
        this.Company.Years = '';
        this.Company.CompanySize='';
        

      },
      handleProject(){
        const Project={
          Title:this.Project.Title,
          Content:this.Project.Content,
          Time:parseInt(this.Project.Time)||0,
          RelatedSkills:this.FilteredSelectedSkill
        };
        this.Projects.push(Project);
        
        this.Project.Title = '';
        this.Project.Content = '';
        this.Project.Time = '';
        this.FilteredSelectedSkill=[];
        

      },
      handleHobi(Interest){
        this.Interests.push(Interest);
        this.Interest='';



      },
      notifyPython() {
        axios.post('http://localhost:5000/api/trigger')
          .then(() => {
            console.log("Python servisi tetiklendi.");
          })
          .catch(error => {
            console.error("Python tetikleme hatası:", error);
          });
      },
      submitForm() {
          const cv = {
              Id: parseInt(this.Id) || 0,
              ActiveUser:parseInt(this.ActiveUser),
              PersonalInfo: {
                  FirstName: this.PersonalInfo.FirstName || '',
                  LastName: this.PersonalInfo.LastName || '',
                  Age: this.PersonalInfo.Age || '',
                  Gender: this.PersonalInfo.Gender || '',
                  Address: this.PersonalInfo.Address || ''
              },
              ContactInfo: {
                  Email: this.ContactInfo.Email || '',
                  Phone: this.ContactInfo.Phone || ''
              },
              EducationInfo: {
                  EduLevel: this.EducationInfo.EduLevel || '',
                  Status: this.EducationInfo.Status || ''
              },
              Companies: this.Companies.length > 0 ? this.Companies : [{CompanyName:'',CompanyField:'',CompanySize:0,Years:0}], 
              PreviousJob: {
                  Name: this.PreviousJob.Name || '',
                  Position: this.PreviousJob.Position || ''
              },
              Skills: this.SelectedSkills || [],
              Interests: this.Interests || [],
              
              ExpectedValues: {
                  locations: this.ExpectedValues.locations || [],
                  expectedSalary: this.ExpectedValues.expectedSalary || 0,
                  expectedWorkHours: this.ExpectedValues.expectedWorkHours || ''
              },
              Languages: this.SelectedLanguages || [],
              Projects: this.Projects.length > 0 ? this.Projects : [{Title:'',Content:'',Time:0,RelatedSkills:[]}], 
              Resume: this.Resume || ""
          };

          // cv objesini kullanarak gerekli işlemleri burada yapabilirsiniz.
          console.log(cv);
          if (this.id) {
            // Düzenleme modu: PUT isteği
            
            axios.put(`http://localhost:7190/cv/`, cv)
              .then(() => {
                this.notifyPython();
                this.$router.push('/employeeHome');
              })
              .catch(err => {
                console.error('Error updating advert:', err);
              });
          } else {
            
            // Yeni ilan oluşturma modu: POST isteği
            axios.post('http://localhost:7190/cv', cv)
              .then(() => {
                this.notifyPython();
                this.$router.push('/employeeHome'  );
              })
              .catch(err => {
                console.error('Error creating advert:', err);
              });
          }
        
        },
        toggleLanguage(Language) {
          const index = this.SelectedLanguages.indexOf(Language);
          if (index === -1) {
            this.SelectedLanguages.push(Language);
          } else {
            this.SelectedLanguages.splice(index, 1);
          }
        },
        
        toggleSkill(Skill) {
          if (this.SelectedSkills.includes(Skill)) {
            // Eğer skill zaten seçildiyse, çıkar
            this.SelectedSkills = this.SelectedSkills.filter(s => s !== Skill);
          } else {
            // Eğer skill seçili değilse, ekle
            this.SelectedSkills.push(Skill);
          }
        },
        toggleFilteredSkill(Skill) {
          const index = this.FilteredSelectedSkill.indexOf(Skill);
          if (index === -1) {
            this.FilteredSelectedSkill.push(Skill);
          } else {
            this.FilteredSelectedSkill.splice(index, 1);
          }
        },
        handleFileUpload(event) {
          const file = event.target.files[0];
          const reader = new FileReader();

          reader.onload = (e) => {
            this.Resume = e.target.result;
          };

          if (file) {
            reader.readAsDataURL(file); // Base64 formatında okur
          }
        },
        }
      };
  </script>
  
  <style>
  
  .form-container {
  max-height: 230px; /* İhtiyaca göre ayarlayın */
  overflow-y: auto;
  padding: 15px;
  border: 1px solid #ddd;
  border-radius: 5px;
  background: #f9f9f9;
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(110px, 1fr));
  grid-template-rows: repeat(auto-fill, minmax(75px, 1fr));
  gap: 10px;
}
  .section-title {
    position: relative;
    display: inline-block;
    font-weight: 500;
    padding-bottom: 0.1rem;    
    border-bottom: 2px solid #333; /* Çizgi rengi ve kalınlığı */
    margin-top: 1rem; /* Başlıklar arasında boşluk */
    margin-bottom: 1rem; /* Başlıklar arasında boşluk */
  }
  .flex-container {
    display: flex;
    flex-wrap: wrap;
  }
  .flex-item {
    margin: 5px;
  }
  </style>
  