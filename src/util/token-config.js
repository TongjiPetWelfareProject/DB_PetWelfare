import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'http://localhost:3000/', // 根据你的API地址配置
  timeout: 5000, // 请求超时时间
});

// 添加请求拦截器，在每个请求中添加JWT令牌
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('jwt_token'); // 从本地存储中获取JWT令牌
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosInstance;
