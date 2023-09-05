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
    console.log(e.response.data)
    if(e.response.data === -1){
        ElMessage({
            type:'warning',
            message:'密码错误，请重新输入'
        })
    }else if (e.response.data === -2) {
        // 处理 e.response.data === -2 的情况
        ElMessage({
          type: 'warning',
          message: '账号已被封禁，请等待解禁',
        });
      } else if (e.response.data === -3) {
        // 处理 e.response.data === -3 的情况
        ElMessage({
          type: 'warning',
          message: '用户不存在',
        });
      } else {
        ElMessage({
            type: 'warning',
            message: '服务器出错',
          });
      }
    
    return Promise.reject(e)
})


export default httpInstance;
