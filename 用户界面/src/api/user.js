// 封装所有和用户相关的接口函数
import request from '@/util/http'

export const loginAPI = ({username,password}) => {
    return request({
        url:'/login',
        method:'POST',
        data:{
            username,
            password
        }
    })
}