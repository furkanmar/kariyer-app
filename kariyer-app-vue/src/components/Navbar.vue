<template>
        <nav 
        :class="[`navbar-${theme.style}`,`bg-${theme.style}`,'navbar','navbar-expand-lg']"
        >
            <div class="container-fluid ">
                <a href="#/home" class="navbar-brand">Kariyer App</a>
                <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                    
                    <li>
                        
                        <router-link
                            v-if="UserType === 'Employer'"

                            to="/employerHome"
                            aria-current="page" 
                            class="nav-link "
                            active-class="page"
                            >Ana sayfa
                        </router-link>


                    </li>
                    <li>
                        
                        <router-link
                            v-if="UserType === 'Admin'"

                            to="/admin"
                            aria-current="page" 
                            class="nav-link "
                            active-class="page"
                            >Ana sayfa
                        </router-link>


                    </li>
                    <li>
                        
                        <router-link
                            v-if="UserType === 'Employee'"

                            to="/employeeHome"
                            aria-current="page" 
                            class="nav-link "
                            active-class="page"
                            >Ana sayfa
                        </router-link>


                    </li>


                    <li>
                        
                        <router-link
                            
                            to="/adverts"
                            aria-current="page" 
                            class="nav-link "
                            active-class="page"
                            v-if="UserType === 'Employer' || UserType === 'Admin'"

                            
                            >İlanlar
                        </router-link>


                    </li>
                
                    <li>
                      
                        
                        <router-link
                            to="/createAdvert"
                            aria-current="page" 
                            class="nav-link "
                            active-class="page"
                            v-if="UserType === 'Employer' || UserType === 'Admin'"

                            
                            >İlan ekleme
                        </router-link>

                    </li>
                    <li>
                        
                        <router-link
                            to="/CvUpload"
                            aria-current="page" 
                            class="nav-link "
                            active-class="page"
                            v-if="UserType === 'Employee' || UserType === 'Admin'"

                            
                            >Özgeçmiş yükle
                        </router-link>
                       

                    </li>
                    
                    
                    
                </ul>
                <form class="d-flex">
                    <button
                    class="btn"
                    :class="{'btn-dark': theme.style === 'light', 'btn-light': theme.style === 'dark'}"

                    >
                    {{ this.UserId }}
                    </button>
                </form>
                <form class="d-flex">
                    <button
                    class="btn"
                    :class="{'btn-dark': theme.style === 'light', 'btn-light': theme.style === 'dark'}"
                    @click.prevent="Exit"
                    >
                    Çıkış
                    </button>
                </form>
                <form class="d-flex">
                    <button
                    class="btn"
                    :class="{'btn-dark': theme.style === 'light', 'btn-light': theme.style === 'dark'}"
                    @click.prevent="changeTheme"
                    >
                    {{ theme.themeWord }}
                    </button>
                </form>
                
            </div>
        </nav>

</template>

<script>

  

export default {
  created() {
    const storedTheme = JSON.parse(localStorage.getItem('theme'));
    if (storedTheme && storedTheme.style && storedTheme.themeWord) {
      this.theme.style = storedTheme.style;
      this.theme.themeWord = storedTheme.themeWord;
    }
  },
  props: {
    user: {
      type: Object,
      default: () => ({})
    }
  },
  
  data() {
    return {
      theme: {
        style: 'dark',
        themeWord: 'Aydınlık Modu'
      },
      UserId:this.user.id,
      UserType:this.user.type,
      

    };
  },
  mounted(){
    this.UserId=localStorage.getItem('ActiveUserId');
    this.UserType=localStorage.getItem('ActiveUserType');
    
  }
  ,
  
  methods: {
    
    Exit(){
      
      this.$emit('exit-success');
      this.$router.push("/");
      
      
      
    },
    changeTheme() {
      if (this.theme.style === 'dark') {
        this.theme.style = 'light';
        this.theme.themeWord = 'Gece Modu';
      } else {
        this.theme.style = 'dark';
        this.theme.themeWord = 'Aydınlık Modu';
      }
      
      localStorage.setItem('theme', JSON.stringify(this.theme));
    }
  }
};
</script>