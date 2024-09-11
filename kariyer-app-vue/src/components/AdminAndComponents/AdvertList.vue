<template>
    
    <div class="container">
    <table class="table">
      <thead>
        <tr>
          <th>ID</th>
          <th>İş Başlığı</th>
          <th>Şirket İsmi</th>
          <th>Lokasyon</th>
          <th>Deneyim(Yıl)</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="advert in adverts" :key="advert.Id">
          <td>{{ advert.Id }}</td>
          <td>{{ advert.jobTitle }}</td>
          <td>{{ advert.companyName }}</td>
          <td>{{ advert.location }}</td>
          <td>{{ advert.experience }}</td>
        </tr>
      </tbody>
    </table>
    </div>
  </template>
  
  
  <script>
 
  import axios from 'axios';
  export default {
    
    data(){
        return{
            adverts:[],
        }
    },
    created(){
        axios.get(`http://localhost:7190/get/GetAdverts`)
        .then(res => {
            this.adverts = res.data;
            console.log(this.adverts);
        })
        .catch(err => {
            console.error('Error fetching adverts:', err);
        });
    },
  };
  </script>
  