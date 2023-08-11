// 封装所有和个人页面用户信息相关的接口函数
import request from '@/util/http'

export const userTotalLikeandReadAPI = ({user_id}) => {
    return request({
        url:'/likeandread',
        method:'POST',
        data:{
            user_id
        }
    })
}

export const userLikePet = ({user_id}) => {
    return request({
        url:'/likepet',
        method:'POST',
        data:{
            user_id
        }
    })
}

export const userCollectPet = ({user_id}) => {
    return request({
        url:'/collectpet',
        method:'POST',
        data:{
            user_id
        }
    })
}

export const userCommentPet = ({user_id}) => {
    return request({
        url:'/commentpet',
        method:'POST',
        data:{
            user_id
        }
    })
}

export const userAdoptPet = ({user_id}) => {
    return request({
        url:'/adoptpet',
        method:'POST',
        data:{
            user_id
        }
    })
}

export const userFosterPet = ({user_id}) => {
    return request({
        url:'/fosterpet',
        method:'POST',
        data:{
            user_id
        }
    })
}