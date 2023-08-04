// axios封装
import axios from 'axios'
import { ElMessage } from 'element-plus'

const httpInstance =axios.create({
    baseURL:'http://localhost:3000/api',
    timeout:5000
})

// 拦截器

// axios请求拦截器
httpInstance.interceptors.request.use(config => {
    return config
},e => Promise.reject(e))

// axios响应式拦截器
httpInstance.interceptors.response.use(res => res.data,e =>{
    // 统一错误提示
    console.log(e.response.data)
    ElMessage({
        type:'warning',
        message:e.response.data
    })
    return Promise.reject(e)
})


export default httpInstance;