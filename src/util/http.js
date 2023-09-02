// axios封装
import axios from 'axios'
import { ElMessage } from 'element-plus'

const httpInstance =axios.create({
    baseURL:'http://101.42.19.77:3000/api',
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
    ElMessage({
        type:'warning',
        message:'服务器错误'
    })
    return Promise.reject(e)
})


export default httpInstance;
