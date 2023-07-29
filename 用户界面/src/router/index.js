import { createRouter, createWebHistory } from 'vue-router';
import pet_foster from '@/components/pet_foster.vue';
import first_page from '@/components/first_page.vue';
import donate from '@/components/donate.vue';
import notice from '@/components/notice.vue'
import medical from '@/components/medical.vue'
import reservationdoctor from '@/components/reservationdoctor.vue'
import pet_adopt from '@/components/pet_adopt.vue'
import pet_details from '@/components/pet_details.vue'
import mypage from '@/components/mypage.vue'
import pet_adopt_form from '@/components/pet_adopt_form.vue'
import forum from '@/components/forum.vue'
import posting from '@/components/posting.vue'
import pet_foster_table from '@/components/pet_foster_table.vue'
import post_details from '@/components/post_details.vue'

const routes = [
    { path: '/', component: pet_foster },
    { path: '/first_page', component: first_page },
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
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
