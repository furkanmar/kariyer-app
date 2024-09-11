<template>
  <div class="container mt-5" v-if="cvs">
    <input 
      type="text" 
      v-model="searchQuery" 
      placeholder="Ara..." 
      class="form-control mb-3"
    />

    <table class="table">
      <thead>
        <tr>
          <th>ID</th>
          <th>E-mail</th>
          <th>İsim isim</th>
          <th>İşlemler</th>
        </tr>
      </thead>
      <tbody>
        <tr 
          v-for="cv in filteredCvs" 
          :key="cv.id"
        >
          <td>{{ cv.id }}</td>
          <td>{{ cv.contactInfo.email }}</td>
          <td>{{ cv.personalInfo.firstName }} {{ cv.personalInfo.lastName }}</td>
          <td><button class="btn btn-danger" @click="deleteCv (cv.id)">Sil</button></td>
        </tr>
      </tbody>
    </table>
  </div>  
</template>

  

<script>
import Navbar from '../Navbar.vue';
import axios from 'axios';

export default {
  components:{
    Navbar
  },
    data(){
        return{
            searchQuery: '',
            cvs:[],
        }
    },
    created(){
        axios.get(`http://localhost:7190/cv`)
        .then(res => {
            this.cvs = res.data;
            console.log(this.cvs);
        })
        .catch(err => {
            console.error('Error fetching adverts:', err);
        });
    },
    methods:{
      notifyPython() {
        axios.post('http://localhost:5000/api/trigger')
          .then(() => {
            console.log("Python servisi tetiklendi.");
          })
          .catch(error => {
            console.error("Python tetikleme hatası:", error);
          });
      },
      deleteCv(id){
        axios.delete(`http://localhost:7190/cv/${id}`)
        this.notifyPython();
        location.reload();  
      }
    },
    computed: {
    filteredCvs() {
      return this.cvs.filter(cv => {
        const searchLower = this.searchQuery.toLowerCase();
        return (
          cv.id.toString().includes(searchLower) ||
          cv.contactInfo.email.toLowerCase().includes(searchLower) ||
          cv.personalInfo.firstName.toLowerCase().includes(searchLower)||
          cv.personalInfo.lastName.toLowerCase().includes(searchLower)

        );
      });
    },
  },
}

</script>