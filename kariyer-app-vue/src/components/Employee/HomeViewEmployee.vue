<template>
  <div class="container mt-2">
    <nav style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);" aria-label="breadcrumb">
        <ol class="breadcrumb">
            
            
            <li class="breadcrumb-item active" aria-current="page" style="font-weight: bold;">Ana sayfa</li>
        </ol>
    </nav>
    <div class="r">
      <h3 class="form-label section-title text-center" >Özgeçmişler</h3>
      
    </div>
    
    <div class="input-group mb-3">
      <label for="labe" class="form-label"></label>
      <input v-model="searchVal" type="text" class="form-control" id="label" placeholder="Arayınız..">
      
    </div>

    
    
    
    <table class="table table-hover">
      <thead>
        <tr>
          <th scope="col-md-4" @click="sortTable('index')">
            CV ID 
            <span v-if="sortKey === 'index'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col-md-4" @click="sortTable('UserId')">
            User ID 
            <span v-if="sortKey === 'UserId'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col-md-4" @click="sortTable('Email')">
            Email 
            <span v-if="sortKey === 'Email'">{{ sortOrder === 'asc' ? '▲' : '▼' }}</span>
          </th>
          <th scope="col-md-4 ">İşlemler</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(cv,index) in sortedCVs" :key="cv.id">
          <td>{{ cv.id || " " }}</td>
          <td>{{ this.ActiveUserId || " " }}</td>
          <td>{{ cv.contactInfo.email|| " " }}</td>

          
          <td>
            <button class="btn btn-primary" @click="editCv(cv.id)">Değiştir</button>
            <button class="btn btn-danger" @click="deleteCv(cv.id)">Sil</button>
            <button class="btn btn-success" @click="pushButton(cv.id)">Eşleşen ilanları görüntüle</button>
            
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
      
  <script>
  import axios from 'axios';
  export default{
        props:{
            id: {
        type: Number,
        required: false
        },
        user: {
        type: Object,
        default: () => ({})
        }
        },
      data(){
          return{
              ActiveUserId:-1,
              ActiveUserType:'',
              adverts:[],
              matchedCv:[],
              cvs:[],
              searchVal:'',
              sortKey: '',
              sortOrder: 'asc',
          }
      },
      computed: {
        filteredCVs() {
          return this.cvs.filter(cv => {
              
              
              const id = cv.id ? cv.id.toString().includes(this.searchVal) : true;
            

              return id
            });
        },
        sortedCVs() {
          return this.filteredCVs.slice().sort((a, b) => {
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
      mounted() {
        this.ActiveUserId=parseInt(localStorage.getItem("ActiveUserId"));
        this.ActiveUserType=localStorage.getItem("ActiveUserType");
        console.log("a");
        axios.get(`http://localhost:7190/cv/getRelatedCv?id=${this.user.id}`)
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
        editCv(id) {
          this.notifyPython();
          this.$router.push({ path: `/editcv/${id}`});
        },
        pushButton(id) {
          try {
            console.log("Eşleşen Profiller butonuna tıklandı.");
            this.notifyPython();
            this.$router.push({ name: 'cv', params: { id } });
            console.log("Rota değişikliği başarılı.");
          } catch (error) {
            console.error("Rota değişikliği sırasında bir hata oluştu:", error);
          }
        },
        sortTable(key) {
          if (this.sortKey === key) {
            this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
          } else {
            this.sortKey = key;
            this.sortOrder = 'asc';
          }
        },
        deleteCv(id){
          
          axios.delete(`http://localhost:7190/cv/${id}`);
          this.notifyPython();
          location.reload();  
        },
        
  
  
        
      },
    
      
  }
  </script>
  