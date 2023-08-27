// 封装所有和个人页面用户信息相关的接口函数
import request from '@/util/http'
import axios from 'axios';

export default {
    userInfoAPI(user_id) {
        return axios
            .post('/api/userinfo',{user_id})
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取个人信息数据时出错：' + error.message);
            });
    }
};

export const userTotalLikeAPI = ({like_num}) => {
    return request({
        url:'/likeandread',
        method:'POST',
        data:{
            like_num
        }
    })
}

export const userReadAPI = ({read_num}) => {
    return request({
        url:'/likeandread',
        method:'POST',
        data:{
            read_num
        }
    })
}

export const userPhonerAPI = ({phone_number}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            phone_number 
        }
    })
}

export const userAddressAPI = ({address}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            address 
        }
    })
}

export const userNameAPI = ({user_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            user_name 
        }
    })
}
//我的被点赞的，被评论的，被收藏的，被浏览的宠物名称
export const userPetLikeAPI = ({mypet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            mypet_name 
        }
    })
}

export const userPetCollectAPI = ({mypet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            mypet_name 
        }
    })
}

export const userPetCommentAPI = ({mypet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            mypet_name
        }
    })
}

export const userPetViewAPI = ({mypet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            mypet_name
        }
    })
}

//我的被点赞的，被评论的，被收藏的，被浏览的宠物名称
export const forumPetLikeAPI = ({pet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_name 
        }
    })
}

export const forumPetCollectAPI = ({pet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_name 
        }
    })
}

export const forumPetCommentAPI = ({pet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_name
        }
    })
}

export const forumPetViewAPI = ({pet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_name
        }
    })
}

//我的领养
export const myAdoptIDAPI = ({pet_id}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_id
        }
    })
}

export const myAdoptPetAPI = ({pet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_name
        }
    })
}

export const myAdoptStartTimeAPI = ({start_time}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            start_time
        }
    })
}

//我的寄养
export const myFosterIDAPI = ({pet_id}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_id
        }
    })
}

export const myFosterPetAPI = ({pet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_name
        }
    })
}

export const myFosterStartYearAPI = ({start_year}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            start_year
        }
    })
}

export const myFosterStartMonthAPI = ({start_month}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            start_month
        }
    })
}

export const myFosterStartDayAPI = ({start_day}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            start_day
        }
    })
}

//我的医疗
export const myTreatIDAPI = ({pet_id}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_id
        }
    })
}

export const myTreatPetAPI = ({pet_name}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            pet_name
        }
    })
}

export const myDocterAPI = ({docter_id}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            docter_id
        }
    })
}

export const myVetIDAPI = ({vet_id}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            vet_id
        }
    })
}

//我的捐款
export const myDonateIDAPI = ({donor_id}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            donor_id
        }
    })
}

export const myDonationAmountAPI = ({donation_amount}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            donation_amount
        }
    })
}

export const myDonationTimeAPI = ({donation_time}) => {
    return request({
        url:'/user_info',//用户信息的后端地址
        method:'POST',
        data:{
            donation_time
        }
    })
}

// function getUserInfo() {
//     return axios.get('/api/user')
//       .then(response => {
//         return response.data;
//       })
//       .catch(error => {
//         console.log(error);
//       });
//   }
