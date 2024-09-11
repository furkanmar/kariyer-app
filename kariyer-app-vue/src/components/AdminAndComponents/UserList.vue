<template>
  <div class="container mt-5" v-if="users">
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
          <th>Kullanıcı Tipi</th>
          <th>İşlemler</th>
        </tr>
      </thead>
      <tbody>
        <tr 
          v-for="user in filteredUsers" 
          :key="user.id"
        >
          <td>{{ user.id }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.type }}</td>
          <td>
            <button class="btn btn-danger" @click="deleteUser(user.id)">Sil</button></td>
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
            users:[],
        }
    },
    created(){
        axios.get(`http://localhost:7190/get/GetUsers`)
        .then(res => {
            this.users = res.data;
            console.log(this.users);
        })
        .catch(err => {
            console.error('Error fetching adverts:', err);
        });
    },
    methods:{
      notifyPython() {
        axios.post('http://localhost:5000/api/trigger')
          .then(() => {
            this.notifyPython();
            console.log("Python servisi tetiklendi.");
          })
          .catch(error => {
            console.error("Python tetikleme hatası:", error);
          });
      },
      deleteUser(id){
        axios.delete(`http://localhost:7190/user/${id}`)
        this.notifyPython();
        location.reload();
      }
    },
    computed: {
    filteredUsers() {
      return this.users.filter(user => {
        const searchLower = this.searchQuery.toLowerCase();
        return (
          user.id.toString().includes(searchLower) ||
          user.email.toLowerCase().includes(searchLower) ||
          user.type.toLowerCase().includes(searchLower)
        );
      });
    },
  },
}

</script>