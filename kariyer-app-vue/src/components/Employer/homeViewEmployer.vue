<template>
  <div class="container mt-2">
    <div class="row">
      <nav style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);" aria-label="breadcrumb">
        <ol class="breadcrumb">
          <li class="breadcrumb-item active " aria-current="page" style="font-weight: bold;">Ana Sayfa</li>
        </ol>
      </nav>
      <h3 class="form-label section-title text-center" >İlanlar</h3>
      <div 
        class="col-md-4 mb-4" 
        v-for="advert in adverts" 
        :key="advert.id"
      >
        <div class="card text-center" style="width: 18rem;">
          <div class="card-body">
            <h5 class="card-title">{{ advert.jobTitle }}</h5>
            <p class="card-text">{{ advert.description ||" "}}</p>
            
            <button class="btn btn-primary"
            @click.prevent="editButton(advert.id)">Eşleşen Profiller</button>
          </div>
        </div>
      </div>
    </div>
  </div>
    
    
</template>
    
<script>
import axios from 'axios';
export default{
    data(){
        return{
            ActiveUserId:-1,
            ActiveUserType:'',
            adverts:[],
            matches:[],
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
    methods:{
      editButton(id) {
        try {
          console.log("Eşleşen Profiller butonuna tıklandı.");
          this.$router.push({ name: 'matches', params: { id } });
          console.log("Rota değişikliği başarılı.");
        } catch (error) {
          console.error("Rota değişikliği sırasında bir hata oluştu:", error);
        }
      },
      


      
    },
    
}
</script>