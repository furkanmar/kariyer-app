<template>
  <div class="container mb-5 mt-5">
    <!-- Logo -->
    <div class="row justify-content-md-center mb-4">
      <div class="col col-lg-6 text-center">
        <img src="/logo.png" alt="Logo" class="logo-img" />
      </div>
    </div>  
    <div class="row justify-content-md-center mb-4">
      <h1 class="form-label text-center">Kariyer App</h1>
    </div>

    <div class="row justify-content-md-center">
      <!-- Giriş Yap -->
      <div class="col col-lg-3">
        <label class="form-label" for="signIn">Giriş yap</label>
        <form class="form-floating mb-3">
          <input type="email" class="form-control" id="floatingInputValue" v-model="EmailSignIn" required>
          <label for="floatingInputValue">E-mail</label>
        </form>
        <form class="form-floating mb-3">
          <input type="password" class="form-control" id="floatingInputValue" v-model="PasswordSignIn" required>
          <label for="floatingInputValue">Şifre</label>
        </form>
        <div class="mb-4">
          <button class="btn btn-success text-uppercase" type="submit" @click.prevent="SignIn">Giriş Yap</button>
        </div>
      </div>

      <!-- Kayıt Ol -->
      <div class="col col-lg-3">
        <label class="form-label" for="signUp">Kayıt ol</label>
        <form class="form-floating mb-3">
          <input type="email" class="form-control" id="floatingInputValue" v-model="EmailSignUP" required>
          <label for="floatingInputValue">E-mail</label>
        </form>
        <form class="form-floating mb-3">
          <input type="password" class="form-control" id="floatingInputValue" v-model="PasswordSignUp" required>
          <label for="floatingInputValue">Şifre</label>
        </form>
        <div class="mb-3 row">
          <div class="col form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="Employer" v-model="TypeSignUp">
            <label class="form-check-label" for="inlineRadio1">İş veren</label>
          </div>
          <div class="col form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="Employee" v-model="TypeSignUp">
            <label class="form-check-label" for="inlineRadio2">İş Arayan</label>
          </div>
        </div>
        <div class="mb-4">
          <button class="btn btn-success text-uppercase" type="button" @click.prevent="SignUp">Kayıt Ol</button>
        </div>
      </div>
    </div>
  </div>
</template>

  
  <script>
  import axios from 'axios';
 

  
  export default {
    data() {
      return {
        EmailSignIn: '',
        PasswordSignIn: '',
        EmailSignUP: '',
        PasswordSignUp: '',
        TypeSignUp:'',
        type:''
       

      }
    },
    
    methods: {
      
      SignIn() {
        const credential={
          Email:this.EmailSignIn,
          Password:this.PasswordSignIn,
        }
        axios.post('http://localhost:7190/user/authenticate', credential)
        .then(response => {
            console.log(response.data);
            const user= response.data;
            
            this.$emit('login-success', user);
            
            

              
            this.$router.push("/home");
        })
        .catch(error => {
            console.error(error);
        }); 
        
      },
      SignUp(){
        const user={
          ID:0,
          Email:this.EmailSignUP,
          Password:this.PasswordSignUp,
          Type:this.TypeSignUp,
          advertsId:[]

        }
        axios.post("http://localhost:7190/user",user)
        .then(() => {
            console.log('Registiration completed Succesfully');
            this.EmailSignUP='';
            this.PasswordSignUp='';
            this.TypeSignUp=''
          })
          .catch(err => {
            console.error('Error creating advert:', err);
          });
          
        

      }
      
    }
 
}

  
  </script>
  <style scoped>
  .logo-img {
    max-width: 200px; /* Resmin genişliği 200px ile sınırlanır */
    height: auto; /* Yükseklik oranı korunarak ayarlanır */
  }
  </style>
  

  