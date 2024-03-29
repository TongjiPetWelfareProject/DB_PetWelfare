import { createRouter, createWebHistory } from 'vue-router';
import { useUserStore } from '@/store/user'; // 引入用户信息存储

//用户界面路由配置
import mainpage from '@/view/用户界面/mainpage.vue';
import pet_foster from '@/components/用户界面/pet_foster.vue';
import first_page from '@/components/用户界面/first_page.vue';
import donate from '@/components/用户界面/donate.vue';
import notice from '@/components/用户界面/notice.vue';
import medical from '@/components/用户界面/medical.vue';
import reservationdoctor from '@/components/用户界面/reservationdoctor.vue';
import pet_adopt from '@/components/用户界面/pet_adopt.vue';
import pet_details from '@/components/用户界面/pet_details.vue';
import mypage from '@/components/用户界面/mypage.vue';
import pet_adopt_form from '@/components/用户界面/pet_adopt_form.vue';
import forum from '@/components/用户界面/forum.vue';
import posting from '@/components/用户界面/posting.vue';
import pet_foster_table from '@/components/用户界面/pet_foster_table.vue';
import post_details from '@/components/用户界面/post_details.vue';

// 管理员界面路由配置
import manager from '@/view/管理员界面/manager.vue';
import MyTableUser from '@/components/管理员界面/mytableuser.vue';
import MyTableEmployee from '@/components/管理员界面/mytableemployee.vue';
import PetCard from '@/components/管理员界面/petcard.vue';
import MyTableDoctor from '@/components/管理员界面/mytabledoctor.vue';
import TableFoster from '@/components/管理员界面/tablefoster.vue';
import TableAdopt from '@/components/管理员界面/tableadopt.vue';
import TableHot from '@/components/管理员界面/tablehot.vue';
import TableNotice from '@/components/管理员界面/tablenotice.vue';
import TableMedical from '@/components/管理员界面/tablemedical.vue';
import TableCheck from '@/components/管理员界面/tablecheck.vue';
import TableRoom from '@/components/管理员界面/tableroom.vue';
import TableDonate from '@/components/管理员界面/tabledonate.vue';

//登录注册路由配置
import login from '@/components/登录注册/Login.vue'
import register from '@/components/登录注册/Register.vue'



const routes = [
    { path: '/', component: mainpage, children:[
      { path: '',component: first_page},
      { path: '/pet_foster', component: pet_foster },
      { path: '/donate', component: donate },
      { path: '/notice', component: notice},
      { path: '/medical', component: medical},
      { path: '/reservationdoctor', component: reservationdoctor},
      { path: '/pet_adopt', component: pet_adopt},
      { name: 'pet_details', path: '/pet_details/:id', component: pet_details},//这是动态路由
      { path: '/mypage', component: mypage},
      { name: 'pet_adopt_form', path: '/pet_adopt_form/:id', component: pet_adopt_form},//这是动态路由
      { path: '/forum', component: forum},
      { path: '/posting', component: posting},
      { path: '/pet_foster_table', component: pet_foster_table},
      { path: '/post_details', component: post_details}
    ] },
    { path: '/login', component: login},
    { path: '/register', component: register},
    { path:'/manager', component: manager,children:[
      { path: '',component: MyTableEmployee},
      { path: '/mytableuser', component: MyTableUser },
      { path: '/mytableemployee', component: MyTableEmployee },
      { path: '/petcard', component: PetCard },
      { path: '/mytabledoctor', component: MyTableDoctor },
      { path: '/tablefoster', component: TableFoster },
      { path: '/tableadopt', component: TableAdopt },
      { path: '/tablehot', component: TableHot },
      { path: '/tablenotice', component: TableNotice },
      { path: '/tabledonate', component: TableDonate },
      { path: '/tablecheck', component: TableCheck },
      { path: '/tablemedical', component: TableMedical },
      { path: '/tableroom', component: TableRoom },
    ]},
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach((to, from, next) => {
  const userStore = useUserStore();
  const userRole = userStore.userInfo ? userStore.userInfo.Role : null;

  if (to.path.startsWith('/manager')||to.path.startsWith('/mytableuser')||to.path.startsWith('/mytableemployee')||to.path.startsWith('/petcard')||to.path.startsWith('/mytabledoctor')||to.path.startsWith('/tablefoster')||to.path.startsWith('/tableadopt')||to.path.startsWith('/tablehot')||to.path.startsWith('/tablenotice')||to.path.startsWith('/tabledonate')||to.path.startsWith('/tablecheck')||to.path.startsWith('/tablemedical')||to.path.startsWith('/tableroom')) {
    // 需要管理权限的路由
    if (!userRole || userRole === 'User') {
      // 如果用户角色为空或为 'User'，跳转到登录页面
      console.log("User")
      next('/login');
    } else {
      console.log("Admin")
      next(); // 允许导航到管理页面
    }
  } else {
    next(); // 其他路由，不需要管理权限
  }
});


export default router;