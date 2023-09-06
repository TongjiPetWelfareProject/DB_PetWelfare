<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import petadopt from '@/api/pet_adopt'

const pets = ref([])
const router = useRouter();

const getpetlist = async () => {
  try {
    const response = await petadopt.getPetList();
    console.log("success");
    console.log(response);

    const uniquePets = {}; // 用于存储已经遍历过的 pet_id

    for (const adoptpet of response) {
      if (!uniquePets[adoptpet.PET_ID]) { // 检查是否已经遍历过该 pet_id
        uniquePets[adoptpet.PET_ID] = true; // 将 pet_id 记录为已遍历
        let gender = '';
        let species = '';
    if (adoptpet.SEX === 'M') {
      gender = '弟弟';
    } else if (adoptpet.SEX === 'F') {
      gender = '妹妹';
    }
    if (adoptpet.SPECIES === 'cat') {
      species = '猫';
    } else if (adoptpet.SPECIES === 'dog') {
      species = '狗';
    }
    console.log(adoptpet.PET_NAME);
    pets.value.push({
      id: adoptpet.PET_ID,
      name: adoptpet.PET_NAME,
      species: species,
      gender: gender,
      age: adoptpet.AGE,
      image: adoptpet.AVATAR
    });
  }
}
  } catch (error) {
    console.error('获取可领养宠物数据时出错：', error);
  }
};


onMounted(() => {
  getpetlist();
});

console.log(pets.value);

const value1 = ref('');
const value2 = ref('');
const value3 = ref('');
const searchName = ref('');
let min = 0;
let max = 100;
const ageRangeMatches = (petAge: number, ageRange: string) => {
  if (ageRange != '11') {
    [min, max] = ageRange.split('-').map(Number);
  } else {
    min = 11; // 默认的最小值
    max = 100; // 默认的最大值
  }
  return petAge >= min && petAge <= max;
};

const filteredPets = computed(() => {
  return pets.value.filter(pet => {
    return (
      (value1.value === '' || pet.species === value1.value) &&
      (value2.value === '' || pet.gender === value2.value) &&
      (value3.value === '' || ageRangeMatches(pet.age, value3.value)) &&
      (searchName.value === '' || pet.name.toLowerCase().includes(searchName.value.toLowerCase()))
    );
  });
});

const options1 = [
  { value: '', label: '不限' },
  { value: '猫', label: '猫' },
  { value: '狗', label: '狗' },
];
const options2 = [
  { value: '', label: '不限' },
  { value: '弟弟', label: '弟弟' },
  { value: '妹妹', label: '妹妹' },
];
const options3 = [
  { value: '', label: '不限' },
  { value: '0-1', label: '0-1岁' },
  { value: '2-3', label: '2-3岁' },
  { value: '4-7', label: '4-7岁' },
  { value: '8-10', label: '8-10岁' },
  { value: '11', label: '11岁及以上' },
];

const goToPet = (pet) => {
      // 跳转到帖子详情页
      console.log('跳转到帖子详情页：' + pet.id);
      router.push({ name: 'pet_details', params: { id: pet.id } });
    };

const currentPage = ref(1)
const pageSize = 8

const handlePageChange = (page) => {
  currentPage.value = page
}

const paginatedPets = computed(() => {
  const startIndex = (currentPage.value - 1) * pageSize
  const endIndex = startIndex + pageSize
  return filteredPets.value.slice(startIndex, endIndex)
})

</script>

<template>
  <div style="display: flex;
  justify-content: center; /* 在水平方向上居中 */
  align-items: center; /* 在垂直方向上居中 */
  /* 其他样式 */">
  <el-row :gutter="20" style="height: 500px; width: 100%; background-color:#4b6fa5;display: flex;
  justify-content: center; /* 在水平方向上居中 */
  align-items: center; 
  ">
    <el-col :span="12" style="height:80%; display: flex;
  justify-content: center; /* 在水平方向上居中 */
  align-items: center; /* 在垂直方向上居中 */">
       <div style="background-color: #4b6fa5;height:95%;padding:20px;border-radius: 10px;">
        <br>
        <br>
         <div style="color: #ccdff5;font-size: 35px;">
          宠物领养
         </div>
         <div style="color: #ffffff;font-size: 50px;font-family:fantasy">
          一段奇妙的生命旅程
         </div>
         <br>
         <div style="color: #ffffff;font-size: 16px;font-family:cursive">
          有一只在角落躲闪的小家伙，正用心静静等待那个将牵起牵绊的人
         </div>
         <div style="color: #ffffff;font-size: 16px;font-family:cursive">
          让我们一同为宠物们开启一扇幸福的大门，让爱在这里延续，温馨永远蔓延
         </div>
        
       </div>
    </el-col>
    <el-col :span="12">
      <img src="./photos/狗互动.jpg" style=" height: 500px;">
    </el-col>
  </el-row></div>
  <div class="background-container">
    <div class="filters-container">
      <label>名字:</label>
        <el-input v-model="searchName" placeholder="输入宠物名字" :style="{ width: '200px' }"></el-input>
        <label>品种:</label>
        <el-select v-model="value1" placeholder="选择品种">
          <el-option
            v-for="item in options1"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <label>性别:</label>
        <el-select v-model="value2" placeholder="选择性别">
          <el-option
            v-for="item in options2"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <label>年龄:</label>
        <el-select v-model="value3" placeholder="选择年龄范围">
          <el-option
            v-for="item in options3"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
    </div>
    <br>
    <div class="pet-list-container">
      <div class="pet-list">
        <div v-for="(pet, index) in paginatedPets" :key="pet.id" class="pet-card" @click="goToPet(pet)">
          <el-card :body-style="{ padding: '0px' }" style="width:100%" class="pet-card-inside">
      <div class="center-container">
      <img v-if="pet.image" :src="pet.image" class="adopt-image">
      <img v-else src="../../../public/home5.jpg" class="adopt-image" alt="Default Image">
      </div>
      <div style="padding: 14px;display: flex;
  justify-content: center;
  flex-direction: column; ">
        <span style="font-size: 18px; color:#4b6fa5;font-weight: bold;">你好呀！我的名字是</span>
        <span style="font-size: 40px; color:#edb055;font-weight: bold;">{{ pet.name }}</span>
        <br>
        <span style="font-size: 18px">{{ pet.species }}{{ pet.gender }}</span>
        <span style="font-size: 16px; color:#6b6a68">{{ pet.age }}岁</span>
        <br>
      </div>
    </el-card>
        </div>
      </div>
    </div>
    <el-pagination
      :current-page="currentPage"
      :page-size="pageSize"
      :total="filteredPets.length"
      layout="prev, pager, next"
      @current-change="handlePageChange"
    ></el-pagination>
  </div>
</template>

<style scoped>
.background-container {
  /* position: fixed; */
  /* top: 0;
  left: 0;
  right: 0;
  bottom: 0; */
  display: flex;
  flex-direction: column; /* 将子元素垂直排列 */
  justify-content: center; /* 在容器中垂直居中对齐 */
  align-items: center; /* 在容器中水平居中对齐 */
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}
.filters-container {
  width: 100%;
  background-color:#eaebeb;/* 设置背景颜色  rgba(166, 219, 225, 0.8)*/
  padding-top: 10px; /* 添加顶部内边距 */
  padding-bottom: 10px; /* 添加底部内边距 */
  border-radius: 4px; /* 设置圆角 */
  margin-bottom: 10px;
  display: flex; /* 使用 flex 布局 */
  justify-content: center; /* 水平居中 */
  align-items: center; /* 垂直居中 */
}

.filters-container > * { /*选择.filters-container元素的所有直接子元素*/
  margin-left: 10px; /* 设置子元素的左边距为10像素 */
  margin-right: 10px; /* 设置子元素的右边距为10像素 */
}

select {
  appearance: none; /* 移除默认样式 */
  padding: 8px; /* 调整内边距 */
  border: none; /* 移除边框 */
  border-radius: 4px; /* 设置圆角 */
  background-color: rgb(213, 136, 49); /* 设置背景颜色为橙色 */
  color: #fff; /* 设置文字颜色为白色 */
  font-size: 14px; /* 设置字体大小 */
}

/* 鼠标悬停样式 */
select:hover {
  background-color: rgb(255, 165, 0); /* 设置悬停时的背景颜色 */
}

/* 聚焦样式 */
select:focus {
  outline: none; /* 移除聚焦时的外边框 */
  box-shadow: 0 0 3px #729cd4; /* 添加聚焦时的阴影效果 */
}

.pet-list-container {
  height: 70%; /* 设置容器的高度 */
  width: 100%;
  overflow-y: auto; /* 设置垂直滚动条 */
}

.pet-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: flex-start;
}
.pet-card {
  display: flex;
  /* height: 200px; */
  width: 24%; /* 设置每个宠物卡片的宽度为占比的48% */
  justify-content: space-between; /*子元素平均分布*/
  margin-right: 10px;
  margin-bottom: 10px;
  border-radius: 4px; /* 设置圆角 */
  cursor: pointer; /* 鼠标悬停时显示手型光标 */
}

.pet-card:hover {
  transform: scale(1.02); /* 放大 2% */
  box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2); /* 添加阴影效果 */
}

/*.pet-card-inside {
  display: flex;
  justify-content: center;
}*/

.pet-details {
  flex: 1; /* 设置其他元素区域占据剩余的可用空间 */
  margin-bottom: 0px; /* 调整每个段落元素（品种、性别、年龄、人气）的底部边距 */
  line-height: 10px;
}

.pet-details p {
  padding-bottom: 0px; 
}

.title{
  height:300px;
  background-image: url("./photos/寄养背景.jpg");
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  display: flex;
}

.center-container {
  display: flex;
  justify-content: center; /* 水平居中 */
  align-items: center; /* 垂直居中 */
}

.adopt-image {
  width: 100%;
  display: block;
  /*max-width: 252.41px;*/ /* 设置最大宽度 */
  width: 252.41px;
  height: 160.45px; /* 设置最大高度 */
  /*width: auto;*/ /* 使宽度自动调整以保持宽高比 */
  border-radius: 10px; /* 设置圆角半径为10像素 */
  margin-top: 10px;
}
</style>
