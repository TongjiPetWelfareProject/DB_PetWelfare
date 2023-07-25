import { createRouter, createWebHistory } from 'vue-router';
import MyTableUser from '@/components/mytableuser.vue';
import MyTableEmployee from '@/components/mytableemployee.vue';
import PetCard from '@/components/petcard.vue';
import MyTableDoctor from '@/components/mytabledoctor.vue';
import TableFoster from '@/components/tablefoster.vue';
import TableAdopt from '@/components/tableadopt.vue';
import TableHot from '@/components/tablehot.vue';
import TableNotice from '@/components/tablenotice.vue';
import TableMedical from '@/components/tablemedical.vue';
import TableCheck from '@/components/tablecheck.vue';
import TableRoom from '@/components/tableroom.vue';
import TableDonate from '@/components/tabledonate.vue';

const routes = [
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
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
