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

export const registerAPI = ({username,password,phoneNumber,city}) => {

    return request({
        url:'/register',
        method:'POST',
        data:{
            username,
            password,
            phoneNumber,
            city
        }
    })
}