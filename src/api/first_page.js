import axiosInstance from '@/util/token-config';
import axios from 'axios';

export default {
    //获取三个领养人气榜宠物的图片，名称
    topAdoptPetsAPI() {
        return axiosInstance
            .get('/api/topAdoptPets')
            .then(response => response.data)
            .catch(error => {
                throw new Error('获取领养宠物人气榜数据时出错：' + error.message);
            });
    }
};