// main.js
import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router'
import Login from './components/Login.vue'
import Register from './components/Register.vue'

const router = createRouter({
    history: createWebHistory(),
    routes: [
      // 路由配置将在下面定义
      {
        path: '/', 
        component: Login
      },
      {
        path: '/login',
        component: Login
      },
      {
        path: '/register',
        component: Register
      }
    ],
  })
  

const app = createApp(App);
app.use(router); // 使用路由
app.mount('#app');