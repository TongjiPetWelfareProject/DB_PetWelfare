<template>
  <div class="block text-center" m="t-4">
    <el-carousel interval="4000" type="card" height="400px">
      <el-carousel-item v-for="(item, index) in images" :key="index">
        <div class="image-container">
          <img class="carousel-image" :src="item" alt="Item Image">
        </div>
      </el-carousel-item>
    </el-carousel>
  </div>
  <el-card class="box-card">
    <template #header>
      <div class="card-header">
        <span>领养</span>
        <el-button class="button" text>查看详情</el-button>
      </div>
    </template>
  </el-card>
  <el-card class="box-card">
    <template #header>
      <div class="card-header">
        <span>寄养</span>
        <el-button class="button" text>查看详情</el-button>
      </div>
    </template>
  </el-card>
  <el-card class="box-card">
    <template #header>
      <div class="card-header">
        <span>关于我们</span>
      </div>
    </template>
    <el-row :gutter="20">
      <el-col :span="8"> <img src="首页1.png" class="image"></el-col>
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
</template>

<script setup>
const images = [
  '11.png',
  '22.png',
  '33.png',
  '44.png'
];
console.log(images[1]);
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
  width:100%;
}

.image {
  width: 100%;
  display: block;
}
</style>


