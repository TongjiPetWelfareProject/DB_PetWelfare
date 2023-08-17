import { createRouter, createWebHistory } from 'vue-router';

//用户界面路由配置
import mainpage from '@/view/用户界面/mainpage.vue';
import pet_foster from '@/components/用户界面/pet_foster.vue';
import first_page from '@/components/用户界面/first_page.vue';
import donate from '@/components/用户界面/donate.vue';
import notice from '@/components/用户界面/notice.vue'
import medical from '@/components/用户界面/medical.vue'
import reservationdoctor from '@/components/用户界面/reservationdoctor.vue'
import pet_adopt from '@/components/用户界面/pet_adopt.vue'
import pet_details from '@/components/用户界面/pet_details.vue'
import mypage from '@/components/用户界面/mypage.vue'
import pet_adopt_form from '@/components/用户界面/pet_adopt_form.vue'
import forum from '@/components/用户界面/forum.vue'
import posting from '@/components/用户界面/posting.vue'
import pet_foster_table from '@/components/用户界面/pet_foster_table.vue'
import post_details from '@/components/用户界面/post_details.vue'

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
      { path: '/pet_details', component: pet_details},
      { path: '/mypage', component: mypage},
      { path: '/pet_adopt_form', component: pet_adopt_form},
      { path: '/forum', component: forum},
      { path: '/posting', component: posting},
      { path: '/pet_foster_table', component: pet_foster_table},
      { path: '/post_details', component: post_details}
    ] },
    { path: '/login', component: login},
    { path: '/register', component: register},
    { path:'/manager', component: manager,children:[
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
    ]}
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
