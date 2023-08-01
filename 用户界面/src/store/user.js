// 管理用户数据

import { defineStore } from "pinia"
import { ref } from "vue"
import { loginAPI } from "@/api/user"


export const useUserStore = defineStore('user',() => {
    const userInfo = ref({})
    const getUserInfo = async({username,password}) => {
        const res = await loginAPI({username,password})
        userInfo.value = res.data
    }
    return {
        userInfo,
        getUserInfo
    }
},{
    persist:true,
})