// 管理用户数据

import { defineStore } from "pinia"
import { ref } from "vue"
import { loginAPI } from "@/api/user"


export const useUserStore = defineStore('user',() => {
    const userInfo = ref({})
    // 获取用户信息并存储
    const getUserInfo = async({username,password}) => {
        const res = await loginAPI({username,password})
        userInfo.value = res.data
    }

    // 退出时清除用户信息
    const clearUserInfo = ()=> {
        userInfo.value={}
    }


    return {
        userInfo,
        getUserInfo,
        clearUserInfo
    }
},{
    persist:true,
})