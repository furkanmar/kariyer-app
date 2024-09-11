<template>
    <div class="container mt-2">
        
        <div class="row">
            <nav style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);" aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="#employerHome" style="color: black;font-weight: bold;">Ana Sayfa</a></li>
                    <li class="breadcrumb-item active" aria-current="page" style="font-weight: bold;">Eşleşmeler</li>
                </ol>
            </nav>
            <h1 class="form-label section-title text-center" >İlan ID:{{ this.id }} için eşleşen Özgeçmişler</h1>
            <div v-if="matches.length > 0" class="row">
                <div 
                    class="col-md-4 mb-4" 
                    v-for="match in matches" 
                    :key="match.id"
                >
                    <div class="card text-center" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title">Cv ID: {{ match.cvId }}</h5>
                        <p class="card-text">Eşleme oranı: %{{ match.score*100 }}</p>
                        <a class="btn btn-primary" @click.prevent="editButton(match.cvId)">
                        Özgeçmişi görüntüle
                        </a>
                    </div>
                    </div>
                </div>
                </div>

            <div class="mt-3" v-else>
                <h3 class="form-label text-center">Bu ilanla eşleşen Özgeçmiş bulunamadı!</h3>
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
            matches:[],
        }
    },
    created(){
        console.log(this.id);
        axios.get(`http://localhost:7190/matches/${this.id}`)
        .then(res => {
            this.matches = res.data;
            console.log(this.matches);
        })
        .catch(err => {
            console.error('Error fetching adverts:', err);
        });


    },
    methods: {
        editButton(cvId) {
            try {
            console.log("Eşleşen CV butonuna tıklandı.");
            this.$router.push({ name: 'cvs', params: { advertId: this.id, cvId } });
            console.log("Rota değişikliği başarılı.");
            } catch (error) {
            console.error("Rota değişikliği sırasında bir hata oluştu:", error);
            }
        }
    },

}
</script>