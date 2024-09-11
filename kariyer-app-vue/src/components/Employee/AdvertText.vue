<template>
  <div class="container mt-2 mb-4" v-if="advert">
    <nav style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);" aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="#employeeHome" style="color: black;font-weight: bold;">Ana Sayfa</a></li>
        <li class="breadcrumb-item"><a :href="`#/cv/${cvId}`" style="color: black;font-weight: bold;">Eşleşmeler</a></li>
        <li class="breadcrumb-item active" aria-current="page" style="font-weight: bold;">İlan Detayları</li>
      </ol>
    </nav>
    <h1 class="text-center">İlan Detayları</h1>

    <!-- Genel Bilgiler -->
    <div class="row">
      <div class="col">
        <h2 class="section-title">Genel Bilgiler</h2>
        <p><strong>İş Başlığı:</strong> {{ advert.jobTitle }}</p>
        <p><strong>Şirket Adı:</strong> {{ advert.companyName }}</p>
        <p><strong>Çalışma Şekli:</strong> {{ advert.workType }}</p>
        <p><strong>Deneyim Yılı:</strong> {{ advert.experience?.years }} Yıl</p>
        <p><strong>Deneyim Gerekliliği:</strong> {{ advert.experience?.xpReq ? 'Evet' : 'Hayır' }}</p>
      </div>
      <div class="col">
        <h2 class="section-title">Şirket Bilgileri</h2>
        <p><strong>Şirket Büyüklüğü:</strong> {{ advert.companySize?.size }}</p>
        <p><strong>Şirket Gerekliliği:</strong> {{ advert.companySize?.compReq ? 'Evet' : 'Hayır' }}</p>
      </div>
    </div>

    <!-- Maaş ve Diğer Bilgiler -->
    <div class="row">
      <div class="col">
        <h2 class="section-title">Maaş ve Diğer Bilgiler</h2>
        <p><strong>Maaş Aralığı:</strong> {{ advert.salary?.min || 'Belirtilmemiş' }} - {{ advert.salary?.max || 'Belirtilmemiş' }}</p>
        <p><strong>Maaş Gerekliliği:</strong> {{ advert.salary?.salReq ? 'Evet' : 'Hayır' }}</p>
        <p><strong>Açıklama:</strong> {{ advert.description }}</p>
        <p><strong>Eğitim Seviyesi Gerekliliği:</strong> {{ advert.educationLevel?.eduReq ? 'Evet' : 'Hayır' }}</p>
      </div>
      <div class="col">
        <!-- Beceriler -->
        <div class="row" v-if="advert.selectedSkills?.skills?.length">
          <div class="col">
            <h2 class="section-title">Beceriler</h2>
            <ul>
              <p v-for="skill in advert.selectedSkills.skills" :key="skill">-{{ skill }}</p>
            </ul>
          </div>
        </div>
        
      </div>
    </div>

    

    <!-- Diller -->
    <div class="row" v-if="advert.selectedLanguages?.languages?.length">
      <div class="col">
        <h2 class="section-title">Diller</h2>
        <ul>
          <p v-for="lang in advert.selectedLanguages.languages" :key="lang">-{{ lang }}</p>
        </ul>
      </div>
      <div class="col">
        <!-- Çalışma Saatleri ve Sertifikalar -->
        <div class="row">
          <div class="col">
            <h2 class="section-title">Çalışma Saatleri ve Sertifikalar</h2>
            <p><strong>Çalışma Saatleri:</strong> {{ advert.workingHours?.hour || 'Belirtilmemiş' }}</p>
            <p><strong>Sertifika:</strong> {{ advert.certification || 'Belirtilmemiş' }}</p>
          </div>
        </div>

      </div>
    </div>

    

    <!-- Lokasyon ve Pozisyon -->
    <div class="row">
      <div class="col">
        <h2 class="section-title">Lokasyon ve Pozisyon</h2>
        <p><strong>Lokasyon:</strong> {{ advert.locations?.locations?.join(', ') }}</p>
        <p><strong>Pozisyon:</strong> {{ advert.position }}</p>
      </div>
      <div class="col">
        <!-- Yayın Durumu -->
        <div class="row">
          <div class="col">
            <h2 class="section-title">Yayın Durumu</h2>
            <p><strong>Yayın Durumu:</strong> {{ advert.isPublished ? 'Yayınlanmış' : 'Yayınlanmamış' }}</p>
          </div>
        </div>
        
      </div>
    </div>
    <div class="row">
      <h2 class="section-title text-center">İlan detayları</h2>
      <p class="text-center">{{ advert.description }}</p>
    </div>
    <div class="d-flex flex-row-reverse">
      <button class="btn btn-success btn-lg"
        style="color: white; text-decoration: none;"
        @click="sendEmail">
        Başvur
      </button>
    </div>
    
  </div>

  <div v-else>
    <p>Veri yükleniyor...</p>
  </div>
</template>

  
  <script>
  import axios from 'axios';
  
  export default {
    props: {
      cvId: {
        type: Number,
        required: true,
      },
      advertId: {
        type: Number,
        required: true,
      },
    },
    data() {
      return {
        advert: null,
        user:null,
      };
    },
    created() {
      console.log(`Advert ID: ${this.advertId}, CV ID: ${this.cvId}`);
      axios
        .get(`http://localhost:7190/get/${this.advertId}`)
        .then((res) => {
          this.advert = res.data;
          console.log(this.advert);
        })
        .catch((err) => {
          console.error('Error fetching adverts:', err);
        });
      axios
      .get(`http://localhost:7190/get/GetEmail?id=${this.advertId}`)
      .then((res) => {
        this.user = res.data;
        console.log(this.user);
      })
      .catch((err) => {
        console.error('Error fetching adverts:', err);
      });
        
    },
    methods: {
      sendEmail() {
        
        const email = this.user.email;
        const subject = 'İş Başvurusu';
        const body = "Merhabalar, Kariyer-app platformundaki " + this.advertId +" ID numaralı iş ilanınıza başvurmak istiyorum.'"; 
        const mailtoLink = `mailto:${email}?subject=${subject}&body=${body}`;
        
        
        window.location.href = mailtoLink;
      },
    },
  };
  </script>
  
  <style scoped>
  .job-detail-container {
    font-family: Arial, sans-serif;
    margin: 20px;
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 8px;
    background-color: #f9f9f9;
  }
  
  h1 {
    color: #2c3e50;
  }
  
  p {
    margin: 8px 0;
  }
  
  ul {
    margin: 0;
    padding: 0;
    list-style: none;
  }
  
  ul li {
    background-color: #ecf0f1;
    margin: 4px 0;
    padding: 8px;
    border-radius: 4px;
  }
  
  h3 {
    margin-top: 20px;
    color: #2980b9;
  }
  </style>
  