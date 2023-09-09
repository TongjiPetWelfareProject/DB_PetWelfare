// axios封装
import axios from 'axios'
import { ElMessage } from 'element-plus'
const baseURLs = [
  'http://101.42.19.77:3000/api',
  'http://tjpetcenter.top:3000/api', // 添加备用URL
];

let currentBaseURLIndex = 0; // 用于跟踪当前使用的URL索引

const httpInstance =axios.create({
  baseURL:baseURLs[currentBaseURLIndex], // 初始使用第一个URL
  timeout:5000
})

// 拦截器

// axios请求拦截器
httpInstance.interceptors.request.use(config => {
  const token = localStorage.getItem('jwt_token');
  if (token) {
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config
},e => Promise.reject(e))

// axios响应式拦截器
httpInstance.interceptors.response.use(res => res.data,e =>{
    // 统一错误提示
    if(e.response.data === -1){
      ElMessage({
        type:'warning',
        message:'密码错误，请重新输入'
      })
    }else if (e.response.data === -2) {
      ElMessage({
        type: 'warning',
        message: '账号已被封禁，请等待解禁',
      });
    } else if (e.response.data === -3) {
      ElMessage({
        type: 'warning',
        message: '用户不存在',
      });
    }else if (e.response.data === -4) {
      ElMessage({
        type: 'warning',
        message: '此手机号已被注册，请重新选择手机号',
      });
    } else {
      ElMessage({
        type: 'warning',
        message: '服务器错误',
      });
      // 请求失败时尝试切换到备用URL
      if (currentBaseURLIndex < baseURLs.length - 1) {
        currentBaseURLIndex++; // 切换到下一个URL
        httpInstance.defaults.baseURL = baseURLs[currentBaseURLIndex];
        // 重新发起请求
        return httpInstance(error.config);
      }
    }

    return Promise.reject(e)
})


export default httpInstance;
