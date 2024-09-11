<template>
    <div class="container mt-5">
        <div class="row">
            <h1 class="form-label section-title text-center" >Özgeçmişleriniz</h1>
        <div 
            class="col-md-4 mb-4" 
            v-for="cv in cvs" 
            :key="cv.Id"
        >
            <div class="card text-center" style="width: 18rem;">
            <div class="card-body">
                <h5 class="card-title">Cv ID:{{  cv.Id}}</h5>
                
                <a class="btn btn-primary"
                @click.prevent="editButton(cv.Id)">CV'yi görüntüle</a>
            </div>
            </div>
        </div>
        
        
        </div>
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
    },
    data(){
        return{
            cvs:[],
        }
    },
    created(){
        console.log(this.id);
        axios.get(`http://localhost:7190/cv/getRelatedCv?id=${this.id}`)
        .then(res => {
            this.cvs = res.data;
            console.log(this.cvs);
        })
        .catch(err => {
            console.error('Error fetching adverts:', err);
        });


    },
    methods: {
        
        editButton(cvId) {
            try {
            console.log("Eşleşen CV butonuna tıklandı.");
            this.$router.push({ name: 'cv', params: { advertId: this.id, cvId } });
            console.log("Rota değişikliği başarılı.");
            } catch (error) {
            console.error("Rota değişikliği sırasında bir hata oluştu:", error);
            }
        }
    },

}
</script>