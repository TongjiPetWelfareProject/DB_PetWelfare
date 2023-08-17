<template>
  <div class="block text-center" m="t-4">
    <el-carousel interval="4000" type="card" height="400px">
      <el-carousel-item style="border-radius:10px" v-for="(item, index) in images" :key="index">
        <div class="image-container">
          <img class="carousel-image" :src="item" alt="Item Image">
        </div>
      </el-carousel-item>
    </el-carousel>
  </div>
  <div class="first_page_container">
    <el-card class="box-card">
      <template #header>
        <div class="card-header">
          <span>领养人气榜</span>
          <el-button class="button" text>查看详情</el-button>
        </div>
      </template>
      <el-row gutter="50">
        <el-col :span="8"><el-card class="card2">
            <div style="padding: 10px">
              <img src="2.png" class="img">
            </div>
            <img src="人气榜2.jpg" class="image">
            <div style="padding: 14px">
              <span>Kitty</span><br>
              <span>热情友善，自信且不怕生</span>
            </div>
          </el-card> </el-col>
        <el-col :span="8"> <el-card class="card1">
            <div style="padding: 10px">
              <img src="1.png" class="img">
            </div>
            <img src="人气榜1.jpg" class="image">
            <div style="padding: 10px ">
              <span>Honey</span><br>
              <span>聪明敏捷，活泼可爱</span>
            </div>
          </el-card></el-col>
        <el-col :span="8"><el-card class="card3">
            <div style="padding: 10px">
              <img src="3.png" class="img">
            </div>
            <img src="人气榜3.jpg" class="image">
            <div style="padding: 10px">
              <span>旺财</span><br>
              <span>乖巧可爱，微笑天使</span>
            </div>
          </el-card> </el-col>
      </el-row>
    </el-card>
    <el-card class="box-card">
      <template #header>
        <div class="card-header">
          <span>寄养</span>
          <el-button class="button" text>查看详情</el-button>
        </div>
      </template>

      <el-row :gutter="20">
        <el-col :span="6">
          <img src="寄养1.png" class="image1">
        </el-col>
        <el-col :span="6">
          <img src="寄养2.png" class="image1">
        </el-col>
        <el-col :span="6">
          <img src="寄养3.png" class="image1">
        </el-col>
        <el-col :span="6">
          <img src="寄养1.png" class="image1">
        </el-col>
      </el-row>

    </el-card>
    <el-card class="box-card">
      <template #header>
        <div class="card-header">
          <span>关于我们</span>
        </div>
      </template>
      <el-row :gutter="20">
        <el-col :span="8"> <img src="关于我们.png" class="image1"></el-col>
        <el-col :span="16">
          <span style="display: block; text-indent: 2em;"> 宠物救助站是一个致力于拯救和保护流浪宠物的组织，我们的使命是提供流浪动物一个温暖、安全的家园。

            宠物救助站是由一群热爱动物的志愿者共同组建而成，我们的团队包括志愿者、兽医、志愿者和管理人员，他们都有着丰富的经验和对动物保护的热忱。

            在宠物救助站，我们接收各类流浪动物，主要是猫狗等。</span>
          <span style="display: block; text-indent: 2em;">
            当动物来到我们的救助站时，我们会先对它们进行健康检查和治疗，确保它们身体健康。同时，我们也会为它们提供食物、水和舒适的居住环境，以满足它们的基本需求。</span>

          <span
            style="display: block; text-indent: 2em;">除了提供基本的生活保障，我们还致力于为流浪宠物寻找合适的领养家庭。我们会对潜在领养者进行严格的筛选，确保他们有足够的爱心和责任心来照顾这些动物。我们也会提供领养后的支持和咨询，帮助新家庭更好地适应和照顾新成员。

            此外，宠物救助站还提供寄养服务和基本医疗服务。在这里，它们可以享受到舒适的居住环境，定期接受健康检查和护理。我们的寄养环境配备了舒适的床铺、宽敞的活动空间和专业的护理人员，确保它们有个温暖、安全的家。

            同时当宠物需要医疗时，兽医团队会进行健康检查、疫苗接种、驱虫和治疗等，确保宠物的身体健康。我们也会处理流浪动物的紧急情况和意外伤害，为它们提供及时的治疗和救助。</span>

          <span style="display: block; text-indent: 2em;">宠物救助站是一个非盈利组织，我们依靠社区的支持和捐赠来维持运作。无论是捐赠还是志愿者的加入，都对我们的工作有着重大的帮助。

            我们相信每个生命都有值得被珍视的权利，通过宠物救助站的努力，我们希望能够改变更多动物的命运，并唤起更多人们对于动物福利的关注和行动。欢迎您加入我们的行列，与我们一起为宠物的幸福和保护而努力！
          </span></el-col></el-row>
    </el-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
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
  '轮播1.png',
  '轮播2.png',
  '轮播3.png',
  '轮播4.png'
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
  height: 100%;
  background-color: #eebe77;
  text-align: center;
  border-radius: 10px;
}

.card2 {
  height: 100%;
  background-color: #f3d19e;
  text-align: center;
  border-radius: 10px;
}

.card3 {
  height: 100%;
  background-color: #fdf6ec;
  text-align: center;
  border-radius: 10px;
}

.image {
  width: 150px;
  height: 150px;
  display: block;
  margin-left: auto;
  margin-right: auto;
  border-radius: 50%;
}

.image1 {
  width: 100%;
  height: 100%;
  display: block;
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
</style>