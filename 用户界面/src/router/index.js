import { createRouter, createWebHistory } from 'vue-router';
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

import login from '@/components/登录注册/Login.vue'
import register from '@/components/登录注册/Register.vue'

const routes = [
    { path: '/', component: first_page },
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
    { path: '/post_details', component: post_details},
    { path: '/login', component: login},
    { path: '/register', component: register},
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
