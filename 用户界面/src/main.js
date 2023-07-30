// import './assets/main.css'
import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import MainApp from './mainpage.vue'
import router from './router'

const mainapp = createApp(MainApp)

mainapp.use(ElementPlus)
mainapp.use(router)
mainapp.mount('#mainapp')