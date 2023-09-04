
<template>
  <div class="block text-center" m="t-4">
    <el-carousel interval="4000" type="card" height="400px" width:900px>
      <el-carousel-item style="border-radius:10px;text-align: center;justify-content: center;"
        v-for="(item, index) in images" :key="index">
        <div class="image-container">
          <img class="carousel-image" :src="item" alt="Item Image">
        </div>
      </el-carousel-item>
    </el-carousel>
  </div>
  <div class="first_page_container">



    <div class="out_container_foster">
      <!-- <br>
        <div class="card-header">
          <div class="title">
            寄养服务
          </div>
        </div>
      <br> -->
      <br>
      <el-row :gutter="20">
        <el-col :span="15">
          <div class="foster_line1">
            <p>
              <span style="font-weight: bold;">寄养服务&nbsp&nbsp;&nbsp</span><span
                style="font-size: 20px;">舒适的家外之家，呵护关爱您的毛小孩</span>
            </p>
          </div>
          <br>
          <div class="foster_line2">
            我们为每一位可爱的宠物提供温馨舒适的环境：
          </div>
          <br>
          <div class="foster_line3">
            · 温暖的床铺
          </div>
          <div class="foster_line3">
            · 宽敞的活动空间
          </div>
          <div class="foster_line3">
            · 专业的照顾人员
          </div>
          <br>
          <br>
          <div>
            <el-button class="my_button" text @click="$router.push('/pet_foster')">查看详情</el-button>
          </div>
        </el-col>
        <el-col :span="9">
          <img src="../../../public/寄养.jpg" class="image1">
        </el-col>
      </el-row>

    </div>


    <div class="out_container">
      <div class="foster_line1">
        <p>
          <span style="font-weight: bold;">领养服务&nbsp&nbsp;&nbsp;</span><span style="font-size: 20px;">用爱呵护，陪伴回家</span>
        </p>
      </div>
      <div class="section-title-big">
        <img src="  ../../../public/crown.png" style="margin-right:20px">
        <span>领养人气榜</span>
        <img src="  ../../../public/crown.png" style="margin-left:20px">
      </div>
      <div class="container" v-if="topPets.length > 0">
        <el-card class="card2">
          <div style="padding: 10px">
            <img src="@/photos/2.png" class="img">
          </div>
          <img :src="topPets[1].image" class="image">
          <div style="padding: 14px">
            <span class="name2">{{ topPets[1].name }}</span><br>
          </div>
        </el-card>
        <el-card class="card1">
          <div style="padding: 10px">
            <img src="@/photos/1.png" class="img">
          </div>
          <img :src="topPets[0].image" class="image">
          <div style="padding: 10px ">
            <span class="name1">{{ topPets[0].name }}</span><br>
          </div>
        </el-card>
        <el-card class="card3">
          <div style="padding: 10px">
            <img src="@/photos/3.png" class="img">
          </div>
          <img :src="topPets[2].image" class="image">
          <div style="padding: 10px">
            <span class="name3">{{ topPets[2].name }}</span><br>
          </div>
        </el-card>
      </div>
      <br>
      <div style="display: flex; justify-content: center;">
        <el-button class="my_button" text @click="$router.push('/pet_adopt')">查看详情</el-button>
      </div>
      <br>
    </div>
    <br>



    <div class="out_container">
      <div class="foster_line1">
        <p>
          <span style="font-weight: bold;">医疗服务&nbsp&nbsp;&nbsp;</span><span style="font-size: 20px;">匠心医疗，关怀宠物健康</span>
        </p>
      </div>
      <div class="medical_line2">
        <div class="medicalimg">
          <img src="../../../public/medicaldog5.png" style="width:300px;height:260px">
        </div>
        <div class="medicaltext">
          <div style="display: flex; align-items: center;">
            <el-icon style="color: #a3c4d9"><Select /></el-icon>
            <span
              style="display: flex; align-items: center; justify-content: center; color:#696969;">深谙医疗之道的专业团队，倾心宠物健康</span>
          </div>
          <div style="display: flex; align-items: center;">
            <el-icon style="color: #a3c4d9"><Select /></el-icon>
            <span
              style="display: flex; align-items: center; justify-content: center; color:#696969;margin-top:6px">匠心医护，高端设施为宠物打造完美疗养体验</span>
          </div>
          <div style="display: flex; align-items: center;">
            <el-icon style="color: #a3c4d9"><Select /></el-icon>
            <span
              style="display: flex; align-items: center; justify-content: center;color:#696969;margin-top:6px">因宠物特性而展开个性呵护，细心满足所需</span>
          </div>
          <div style="display: flex; align-items: center;">
            <el-icon style="color: #a3c4d9"><Select /></el-icon>
            <span
              style="display: flex; align-items: center; justify-content: center;color:#696969;margin-top:6px">注重医疗细节，呵护宠物健康，我们执着而细心</span>
          </div>
          <div style="display: flex; align-items: center;">
            <el-icon style="color: #a3c4d9"><Select /></el-icon>
            <span
              style="display: flex; align-items: center; justify-content: center;color:#696969;margin-top:6px">医疗创新点亮未来，为宠物的健康谱写精彩篇章</span>
          </div>
          <!-- <div style="display: flex; align-items: center;">
            <el-icon style="color: #a3c4d9"><Select/></el-icon>
            <span style="display: flex; align-items: center; justify-content: center;color:#696969;margin-top:6px">心系医护，共同守护宠物幸福的每一刻</span>
          </div> -->
          <br>
          <div>
            <el-button class="my_button" text @click="$router.push('/medical')">查看详情</el-button>
          </div>
        </div>
      </div>
      <br>
    </div>


    <div class="out_container">
      <!-- <template #header> -->
      <br>
      <div class="foster_line1">
        <p>
          <span style="font-weight: bold;">关于我们&nbsp&nbsp;&nbsp;</span><span style="font-size: 20px;">用爱心，创造温馨</span>
        </p>
      </div>
      <br>
      <br>
      <!-- </template> -->
      <el-row :gutter="20">
        <!-- <el-col :span="9"> <img src="关于我们.png" class="image1"></el-col> -->
        <el-col :span="6">
          <el-card class="box-card">
            <template #header>
              <div class="card-header">
                <div class="foster_line4">
                  我们的使命
                </div>
                <!-- <el-button class="button" text>Operation button</el-button> -->
              </div>
            </template>
            <div class="foster_line5">
              提供流浪动物一个温暖、安全的家园
            </div>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card class="box-card">
            <template #header>
              <div class="card-header">
                <div class="foster_line4">
                  我们的团队
                </div>
                <!-- <el-button class="button" text>Operation button</el-button> -->
              </div>
            </template>
            <div class="foster_line5">
              有着丰富经验和对动物保护热忱的兽医、志愿者和管理人员
            </div>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card class="box-card">
            <template #header>
              <div class="card-header">
                <div class="foster_line4">
                  我们的工作
                </div>
                <!-- <el-button class="button" text>Operation button</el-button> -->
              </div>
            </template>
            <div class="foster_line5">
              · 提供基本的生活保障与医疗服务
            </div>
            <div class="foster_line5">
              · 为流浪宠物寻找合适的领养家庭
            </div>
            <div class="foster_line5">
              · 提供宠物寄养服务
            </div>
          </el-card>
        </el-col>
        <el-col :span="6">
          <el-card class="box-card">
            <template #header>
              <div class="card-header">
                <div class="foster_line4">
                  我们需要你
                </div>
                <!-- <el-button class="button" text>Operation button</el-button> -->
              </div>
            </template>
            <div class="foster_line5">
              我们依靠社区的支持和捐赠来维持运作
            </div>
            <div class="foster_line5">
              无论是捐赠还是志愿者的加入，都对我们的工作有着重大的帮助
            </div>
          </el-card>
        </el-col>

      </el-row>
      <br>
      <br>
      <el-row>

        <el-col :span="16">
          <div class="foster_line4">
            我们相信
          </div>
          <br>
          <div class="foster_line0">
            每个生命都有值得被珍视的权利
          </div>
          <br>
          <div class="foster_line6">
            让我们用爱与关怀去呵护每一个生命的绽放
          </div>
        </el-col>
        <el-col :span="8"><img src="../../../public/petcare.jpg" class="image1"></el-col>
      </el-row>
      <br>
    </div>





  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { StarFilled, Check, Select } from '@element-plus/icons-vue'
import first_page from '@/api/first_page';

const topPets = ref([]);

// 在组件挂载后获取领养宠物人气榜数据
onMounted(async () => {
  try {
    topPets.value = await first_page.topAdoptPetsAPI();
  } catch (error) {
    console.error('获取领养宠物人气榜数据时出错：', error);
  }
});

const images = [
  'home3.png',
  'home5.jpg',
  'home7.jpg',
  'home6.jpg',
  'home4.jpg',
  'home8.jpg',
];

</script>


<style scoped>
.demonstration {
  color: var(--el-text-color-secondary);
}

.el-carousel__item h3 {
  color: #475669;
  opacity: 0.75;
  line-height: 200px;
  margin: 0;
  text-align: center;
}


.image-container {
  width: 100%;
  height: 100%;
  overflow: hidden;
}

.carousel-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.box-card {
  width: 80%;
}

.card1 {
  width: 300px;
  height: 100%;
  text-align: center;
  border-radius: 10px;
  flex: 1;
  margin-left: 30px;
  margin-right: 30px;
  /* box-shadow: 0 2px 4px rgba(234, 82, 11, 0.517); */
  background-color: #deeaf9;
}

.card2 {
  width: 300px;
  height: 100%;
  text-align: center;
  border-radius: 10px;
  flex: 1;
  margin-left: 50px;
  margin-right: 50px;
  /* box-shadow: 0 2px 4px rgba(234, 160, 11, 0.517); */
  background-color: #deeaf9;
}

.card3 {
  width: 300px;
  height: 100%;
  text-align: center;
  border-radius: 10px;
  flex: 1;
  margin-left: 50px;
  margin-right: 50px;
  /* box-shadow: 0 2px 4px rgba(255, 231, 45, 0.517); */
  background-color: #deeaf9;
}

/* 当用户悬停在卡片元素上时应用的样式 */
.el-card:hover {
  background-color: white;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
  transform: scale(1.05);
}

.name1 {
  color: #ffbd5a;
  font-weight: bold;
  font-size: 28px;
}

.name2 {
  color: #878686;
  font-weight: bold;
  font-size: 28px;
}

.name3 {
  color: #b68b50;
  font-weight: bold;
  font-size: 28px;
}


.image1 {
  width: 100%;
  height: 100%;
  display: block;
}

.image {
  width: 200px;
  height: 200px;
  display: block;
  margin-left: auto;
  margin-right: auto;
  border-radius: 50%;
}

.img {
  width: 50px;
  height: 50px;
  display: block;
  margin-left: auto;
  margin-right: auto;
  border-radius: 50%;
}


.first_page_container {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  /* 将子元素垂直排列 */
}

.out_container {
  width: 80%;

  border-top: 1px solid #dad9d9;
}


.out_container_foster {
  width: 80%;
}

.title {
  font-size: 27px;
  color: #729cd4;
  margin-left: -12px
}

.my_button {
  background-color: #eebe77;
  color: #fff;
  padding: 10px 20px;
  border: none;
  /* 去除边框 */
  border-radius: 5px;
  /* color:#eebe77 */
}

/* 添加悬停效果 */
.my_button:hover {
  background-color: blue; /* 修改背景颜色 */
  color: #eebe77; /* 修改文本颜色 */
  transform: scale(1.05); /* 稍微放大按钮 */
}

.foster_line0 {
  font-size: 30px;
  color: #f6ab3b;
  font-weight: bold;
}

.foster_line1 {
  font-size: 32px;
  color: #729cd4;
}

.foster_line2 {
  font-size: 18px;
  color: #696969;
}

.foster_line3 {
  font-size: 16px;
  color: #696969;
}

.foster_line4 {
  font-size: 22px;
  color: #729cd4;
}

.foster_line5 {
  font-size: 14px;
  color: #000407;
}

.foster_line6 {
  font-size: 23px;
  color: #eebe77;
  font-weight: bold;
}

.medical_line2 {
  display: flex;
  align-items: center;
  justify-content: center;
}

.medicalimg {
  margin-right: 300px;
}

.medicaltext {
  font-size: 15px;
  color: #000407;
}

.box-card {
  width: 100%;
  height: 200px;
}

.topAdopt {
  display: flex;
  flex-direction: column;
  align-items: center;
  flex-direction: column;
  border-top: 1px solid #dad9d9;
  /* border-bottom: 1px solid #9a89bb; */
  padding-top: 4.5em;
  padding-bottom: 3em;
}


.section-title-big {
  font-size: 22px;
  line-height: 32px;
  font-weight: 700;
  padding-bottom: 16px;
  color: #9a89bb;
  text-align: center;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>


