import { createRouter, createWebHashHistory, routeLocationKey } from 'vue-router';
import AdvertView from './components/Employer/advertView.vue';
import CreateAdvert from './components/Employer/createAdvert.vue';
import CvListView from './components/Employer/CvListView.vue';
import HomeViewEmployer from './components/Employer/homeViewEmployer.vue';
import MatchesView from './components/Employer/matchesView.vue';
import CvUpload from './components/Employee/CvUpload.vue';
import HomeView from './components/HomeView.vue';
import Login from './components/Login.vue';
import HomeViewEmployee from './components/Employee/HomeViewEmployee.vue';
import AdminView from './components/AdminAndComponents/AdminView.vue';
import UserList from './components/AdminAndComponents/UserList.vue';
import AdvertList from './components/AdminAndComponents/AdvertList.vue';
import CvList from './components/AdminAndComponents/CvList.vue';
import CvView from './components/Employee/CvView.vue';
import MatchesAdvert from './components/Employee/matchesAdvert.vue';
import AdvertText from './components/Employee/AdvertText.vue';


const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    { path: "/", name: "Login", component: Login },
    { path: '/createAdvert', component: CreateAdvert },
    {
      path: '/edit/:id',
      name: 'EditAdvert',
      component: CreateAdvert,
      props: route => ({ id: Number(route.params.id), editMode: true })
    },
    {
      path: '/editcv/:id',
      name: 'EditCV',
      component: CvUpload,
      props: route => ({ id: Number(route.params.id), editMode: true })
    },
    { path: '/adverts', component: AdvertView },
    { path:'/employerHome', component:HomeViewEmployer },
    { path: '/home', component: HomeView },
    { path: '/CvUpload', component: CvUpload },
    { path: '/employeeHome',name:'employeehome', component:HomeViewEmployee, props:route=> ({id:Number(route.params.id)})},
    { path: '/admin', name: 'AdminView', component: AdminView },
    { path: '/admin/users', name: 'UserList', component: UserList },
    { path: '/admin/adverts', name: 'AdvertList', component: AdvertList },
    { path: '/admin/cvs', name: 'CvList', component: CvList },
    { path: "/matches/:id", name: "matches", component: MatchesView, props: route => ({ id: Number(route.params.id)}) },
    { path: "/matches/:advertId/cv/:cvId", name: "cvs",component: CvListView,props: route => ({advertId: Number(route.params.advertId),cvId: Number(route.params.cvId)})},
    { path: "/cv, ", component: CvView },
    { path: "/cv/:id", name: "cv", component: MatchesAdvert, props: route => ({ id: Number(route.params.id)}) },
    { path: "/matches/:cvId/advert/:advertId", name: "adverts",component: AdvertText,props: route => ({cvId: Number(route.params.cvId),advertId: Number(route.params.advertId)})},


  ]
});

export default router;
