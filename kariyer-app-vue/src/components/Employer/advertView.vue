<template>
  
  
  <div class="container mt-5">
    <div class="input-group mb-3">
      <input v-model="searchTitle" type="text" class="form-control" placeholder="Başlığa göre filtrele">
      <span class="input-group-addon"></span>
      <input v-model="searchLocation" type="text" class="form-control" placeholder="Şehre göre filtrele">
      <span class="input-group-addon"></span>
      <input v-model="searchYear" type="text" class="form-control" placeholder="Yıla göre filtrele">
      <span class="input-group-addon"></span>
    </div>

    
    <div class="mb-3">
      <div class="form-check">
        <input v-model="showPublished" type="checkbox" class="form-check-input" id="publishedCheck">
        <label class="form-check-label" for="publishedCheck">Yayınlanmış</label>
      </div>
      <div class="form-check">
        <input v-model="showUnpublished" type="checkbox" class="form-check-input" id="unpublishedCheck">
        <label class="form-check-label" for="unpublishedCheck">Yayınlanmamış</label>
      </div>
    </div>
    
    <table class="table table-hover">
      <thead>
        <tr>
          <th scope="col" @click="sortTable('index')">
            Form ID 
            <span v-if="sortKey === 'index'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col" @click="sortTable('UserId')">
            User ID 
            <span v-if="sortKey === 'UserId'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>

          <th scope="col" @click="sortTable('jobTitle')">
            İş Başlığı 
            <span v-if="sortKey === 'jobTitle'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col" @click="sortTable('locations')">
            Konum
            <span v-if="sortKey === 'locations'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col" @click="sortTable('experience')">
            Deneyim
            <span v-if="sortKey === 'experience'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col" @click="sortTable('isPublished')">
            Yayınlanma Durumu
            <span v-if="sortKey === 'isPublished'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col">İşlemler</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(advert,index) in sortedAdverts" :key="advert.id">
          <td>{{ advert.id || " " }}</td>
          <td>{{ advert.activeUser|| " " }}</td>
          <td>{{ advert.jobTitle || " " }}</td>
          <td>{{ advert.locations.locations.length > 0 ? advert.locations.locations.join(", ") : " " }}</td>
          <td>{{ advert.experience.years != null ? advert.experience.years + ' Yıl' : " " }}</td>
          <td>{{ advert.isPublished != null ? (advert.isPublished ? "Yayınlandı" : "Yayınlanmadı") : " " }}</td>
          <td>
            <button class="btn btn-primary" @click="editAdvert(advert.id)">Değiştir</button>
            <button class="btn btn-danger" @click="deleteAdvert(advert.id)">Sil</button>
            <button type="button" class="btn btn-warning" @click="togglePublication(advert.id)">
              {{ advert.isPublished ? "Yayından kaldır" : "Yayınla" }}
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>

import axios from 'axios';

export default {
  
  data() {
    return {
      adverts: [],
      sortKey: '',
      sortOrder: 'asc',
      searchTitle: '',
      searchLocation: '',
      searchYear: '',
      selectedDeletion:null,
      showPublished: false,
      showUnpublished: false,
      ActiveUserId:0,
      ActiveUserType:'',
    };
  },
  computed: {
    filteredAdverts() {
      return this.adverts.filter(advert => {
          const titleMatch = advert.jobTitle ? advert.jobTitle.toLowerCase().includes(this.searchTitle.toLowerCase()) : true;
          const locationMatch = advert.locations.locations && advert.locations.locations.length > 0 ? advert.locations.locations.some(location => location.toLowerCase().includes(this.searchLocation.toLowerCase())) : true;
          const yearMatch = advert.experience.years ? advert.experience.years.toString().includes(this.searchYear) : true;
          const publishMatch = (this.showPublished && advert.isPublished) || (this.showUnpublished && !advert.isPublished) || (!this.showPublished && !this.showUnpublished);

          return titleMatch && locationMatch && yearMatch && publishMatch;
        });
    },
    sortedAdverts() {
      return this.filteredAdverts.slice().sort((a, b) => {
        let modifier = this.sortOrder === 'asc' ? 1 : -1;
        if (this.sortKey === 'index') {
          return (a.index - b.index) * modifier;
        }
        if (a[this.sortKey] < b[this.sortKey]) return -1 * modifier;
        if (a[this.sortKey] > b[this.sortKey]) return 1 * modifier;
        return 0;
      });
    }
  },

  created() {
    this.ActiveUserId=localStorage.getItem("ActiveUserId");
    this.ActiveUserType=localStorage.getItem("ActiveUserType");
    axios.get(`http://localhost:7190/advert/${this.ActiveUserId}`)
      .then(res => {
        this.adverts = res.data;
        console.log(this.adverts);
      })
      .catch(err => {
        console.error('Error fetching adverts:', err);
      });
  },
  mounted() {
      this.ActiveUserId=localStorage.getItem("ActiveUserId");
      this.ActiveUserType=localStorage.getItem("ActiveUserType");
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
    
    sortTable(key) {
      if (this.sortKey === key) {
        this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
      } else {
        this.sortKey = key;
        this.sortOrder = 'asc';
      }
    },
    deleteAdvert(id) {
      
      axios.delete(`http://localhost:7190/advert/${id}`)
      .catch(err => {
            this.notifyPython();
            console.error('Error creating advert:', err);
          });
      this.$router.push("/adverts");
      location.reload();
    },
    editAdvert(id) {
      this.$router.push({ path: `/edit/${id}`});
    },
    togglePublication(id) {
      const advert = this.adverts.find(advert => advert.id === id);
      if (advert) {
        advert.isPublished = !advert.isPublished;
        axios.put(`http://localhost:7190/advert/`, advert)
          .then(() => {
            // Başarıyla güncellendiğinde yapılacak işlemler
          })
          .catch(err => {
            console.error('Error updating advert publication status:', err);
          });
      }
    }
  
  }
};
</script>

