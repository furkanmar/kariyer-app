<template>
    <div class="container mt-2 mb-4" v-if="cv">
      <nav style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item" ><a href="#employerHome" style="color: black;font-weight: bold;" >Ana Sayfa</a></li>
          <li class="breadcrumb-item"><a :href="`#/matches/${advertId}`" style="color: black;font-weight: bold;">Eşleşmeler</a></li>
          <li class="breadcrumb-item active" aria-current="page" style="font-weight: bold;">Özgeçmiş</li>
        </ol>
      </nav>
      <h1 class="text-center">CV Detayları</h1>
  
      <!-- Kişisel Bilgiler -->
        <div class="row">
            <div  class="col"  >
                <h2 class="section-title">Kişisel Bilgiler</h2>
                <p><strong>Ad:</strong> {{ cv.personalInfo.firstName }} {{ cv.personalInfo.lastName ||' '}}</p>
                <p><strong>Yaş:</strong> {{ cv.personalInfo.age }}</p>
                <p><strong>Cinsiyet:</strong> {{ cv.personalInfo.gender ||' '}}</p>
                <p><strong>Adres:</strong> {{ cv.personalInfo.address ||' '}}</p>
            </div>
        
            <!-- İletişim Bilgileri -->
            <div class="col">
                <h2 class="section-title">İletişim Bilgileri</h2>
                <p><strong>Email:</strong> {{ cv.contactInfo.email }}</p>
                <p><strong>Telefon:</strong> {{ cv.contactInfo.phone }}</p>
            </div>
        </div>
        
  
      <!-- Eğitim Bilgileri -->
      <div class="row" v-if="cv.educationInfo">
        <div class="col">
          <h2 class="section-title">Eğitim Bilgileri</h2>
          <p><strong>Son Mezuniyet Derecesi:</strong> {{ cv.educationInfo.EduLevel }}</p>
          <p><strong>Eğitim Durumu:</strong> {{ cv.educationInfo.Status }}</p>
        </div>
        <!-- Diller -->
        <div class="col">
          <h2 class="section-title">Diller</h2>
          <ul>
            <li v-for="(language, index) in cv.languages" :key="index">{{ language }}</li>
          </ul>
        </div>

      </div>

      <!-- Beklentiler -->
      <div class="row ">
        <div class="col ">
        
          <h2 class="section-title text-center">Beklentiler</h2>
          <p><strong>Beklenen Maaş:</strong> {{ cv.expectedValues.expectedSalary ? cv.expectedValues.expectedSalary + ' ₺' : 'Belirtilmemiş' }}</p>
          <p><strong>Beklenen Çalışma Saatleri:</strong> {{ cv.expectedValues.expectedWorkHours || 'Belirtilmemiş' }}</p>
          <p><strong>Çalışmak İstediği Şehirler:</strong> {{ cv.expectedValues.locations.length > 0 ? cv.expectedValues.locations.join(', ') : 'Belirtilmemiş' }}</p>
        <
        </div>
        <!-- Önceki İş -->
        <div class="col ">
            
              <h2 class="section-title text-center">Önceki İş</h2>
              <p><strong>İş Adı:</strong> {{ cv.previousJob.name }}</p>
              <p><strong>Pozisyon:</strong> {{ cv.previousJob.position }}</p>
            <
          
          
        </div>
      </div>
      <!-- Yetenekler ve ilgi alanları -->
      <div class="row">
        <div class="col">
          <h2 class="section-title text-center">Yetenekler</h2>
          <ul>
            <li  v-for="(skill, index) in cv.skills" :key="index">{{ skill }}</li>
          </ul>
        </div>
        <div class="col">
            <h2 class="section-title">İlgi Alanları</h2>
          <ul>
            <li v-for="(interest, index) in cv.interests" :key="index">{{ interest }}</li>
          </ul>
        </div>
      </div>
  
      <!-- Şirketler -->
      <div class="row" v-if="cv.companies && cv.companies.length > 0">
        <h2 class="section-title">Şirket Deneyimleri</h2>
        <div v-for="(company, index) in cv.companies" :key="index" class="col">
          <h4>{{ company.companyName }}</h4>
          <p><strong>Alan:</strong> {{ company.companyField }}</p>
          <p><strong>Şirket Büyüklüğü:</strong> {{ company.companySize }}</p>
          <p><strong>Çalışma Süresi:</strong> {{ company.years }} yıl</p>
        </div>
      </div>

  
      <!-- Projeler -->
      <div class="row" v-if="cv.projects && cv.projects.length > 0">
        <h2 class="section-title">Projeler</h2>
        <div v-for="(project, index) in cv.projects" :key="index" class="col">
          <h4>{{ project.title }}</h4>
          <p>{{ project.content }}</p>
          <p><strong>Süre:</strong> {{ project.time }} Yıl</p>
          <p><strong>İlgili Yetenekler:</strong> {{ project.relatedSkills.join(', ') }}</p>
        </div>
      </div>
      
  
      <!-- Özgeçmiş -->
      <div class="row">
        <h2 class="section-title">Özgeçmiş</h2>
        <p>{{ cv.resume }}</p>
      </div>
      <div class="d-flex flex-row-reverse">
      <button class="btn btn-success btn-lg"
        style="color: white; text-decoration: none;"
        @click="sendEmail">
        Mail At
      </button>
    </div>
    </div>
  </template>

<script>
import axios from 'axios';
export default{
      props: {
          advertId: {
          type: Number,
          required: true
          },
          cvId: {
          type: Number,
          required: true
          }
      },
    data(){
        return{
            cv:null,
        }
    },
    created() {
        console.log(`Advert ID: ${this.advertId}, CV ID: ${this.cvId}`);
        axios.get(`http://localhost:7190/cv/${this.cvId}`)
        .then(res => {
            this.cv = res.data;
            console.log(this.cv);
        })
        .catch(err => {
            console.error('Error fetching adverts:', err);
        });
    },
    methods: {
      sendEmail() {
        
        const email = this.cv.contactInfo.email;
        const subject = 'İş Başvurusu';
        const body = "Merhabalar, Kariyer-app platformundaki " + this.cvId +" ID numaralı Özgeçmişinize iş teklifinde bulunmak isteriz.'"; 
        const mailtoLink = `mailto:${email}?subject=${subject}&body=${body}`;
        
        
        window.location.href = mailtoLink;
      },
    },

}
</script>
<style scoped>
.container {
  max-width: 800px;
  margin: auto;
}
.section {
  margin-bottom: 30px;
}
h1, h2, h4 {
  color: #2c3e50;
}
</style>