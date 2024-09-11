<template>
  <div class="app-container">
    <!-- Navbar always on top -->
    <Navbar 
      v-if="showNavbar" 
      :user="user"
      @exit-success="handleExit"
    />

    <!-- Main content area with white background and scrolling -->
    <div class="content-area">
      <router-view
        :user="user"
        @login-success="handleLoginSuccess"
      />
    </div>

    <!-- Background image covering the whole viewport -->
    <div class="background-image"></div>
  </div>
</template>

<script>
import Navbar from './components/Navbar.vue';

export default {
  components: {
    Navbar
  },
  data() {
    return {
      showNavbar: false,
      user: null,
    }
  },
  created() {
    const activeUserId = localStorage.getItem("ActiveUserId");
    const activeUserType = localStorage.getItem("ActiveUserType");
    
    if (activeUserId && activeUserId !== '-1') {
      this.user = {
        id: activeUserId,
        type: activeUserType
      };
      this.showNavbar = true;
    }
  },
  methods: {
    handleLoginSuccess(user) {
      this.user = user;
      localStorage.setItem("ActiveUserId", user.id);
      localStorage.setItem("ActiveUserType", user.type);
      
      this.showNavbar = true;
      this.$router.push('/home');
    },
    handleExit() {
      localStorage.setItem("ActiveUserId", '-1');
      localStorage.setItem("ActiveUserType", '');
      this.showNavbar = false;
    },
  }
}
</script>

<style scoped>
html, body {
  margin: 0;
  padding: 0;
  height: 100%;
}

.app-container {
  display: flex;
  flex-direction: column;
  height: 100vh;
  position: relative;
}

.background-image {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-image: url('@/../public/stairs.jpg'); /* Arka plan resmi yolu */
  background-size: cover; /* Resmi ekrana tam olarak kaplar */
  background-position: center;
  background-repeat: no-repeat;
  z-index: -1; /* Arka planı içerikten arka plana koyar */
}

.content-area {
  margin: 0 auto; /* Ortalamak için */
  width: 85%; /* İçerik alanının genişliği %85 */
  background-color: rgb(244, 247, 245); /* Beyaz arka plan */
  height: 100%; /* Yüksekliği doldurur */
  overflow-y: auto; /* Kaydırma desteği */
  position: relative;
  padding: 20px; /* İçerik alanına biraz boşluk ekleyebiliriz */
}
</style>
