<template>
    
    <div class="container ">
        <div class="row mt-5">
        <div class="col">
            <div class="card text-center" style="width: 20rem;">
            <div class="card-body">
                <h5 class="card-title">Kullanıcılar</h5>
                <router-link to="/admin/users" class="btn btn-primary">Listeyi Görüntüle</router-link>
            </div>
            </div>
        </div>
        <div class="col">
            <div class="card text-center" style="width: 20rem;">
            <div class="card-body">
                <h5 class="card-title">İlanlar</h5>
                <router-link to="/adverts" class="btn btn-primary">Listeyi Görüntüle</router-link>
            </div>
            </div>
        </div>
        <div class="col">
            <div class="card text-center" style="width: 20rem;">
            <div class="card-body">
                <h5 class="card-title">Özgeçmişler</h5>
                <router-link to="/admin/cvs" class="btn btn-primary">Listeyi Görüntüle</router-link>
            </div>
            </div>
        </div>
        </div>
    </div>
</template>


<script>

export default {
    
}
</script>
