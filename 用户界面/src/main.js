// import './assets/main.css'
import { createApp,provide } from 'vue'
import { createPinia } from 'pinia'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import App from './App.vue'
import router from './router'

const app = createApp(App)
const pinia = createPinia()

app.use(ElementPlus)
app.use(router)
// app.use(pinia)
app.mount('#app')