<template>
  <el-card class="box-card">
    <div class="describe">
      捐助范围:<br>

      1、我们非常乐意接受爱心朋友们经常性的捐助<br>

      2.、我们也非常希望爱心朋友们能为小猫小狗们捐出您的时间.<br>

      3、我们只接受现金的捐助.<br>

      备注: 我们会对您的捐助做好记录, 并定期在网站上公布.<br>

      我们诚挚地感谢捐助者和捐助团体.<br>
    </div>
  </el-card>
  <el-button type="primary" text @click="open">点击捐款</el-button>
</template>

<style scoped>
.text {
  font-size: 14px;
}

.item {
  padding: 18px 0;
}

.box-card {
  width: 800px;
}

.describe {
  font-size: 14px;
  color: #312e2c;
  line-height: 26px;
}
</style>

<script lang="ts" setup>
import { ElMessage, ElMessageBox } from 'element-plus'

const open = () => {
  ElMessageBox.prompt('请输入捐款内容', '捐款', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    inputPattern: /^[\u4e00-\u9fa5a-zA-Z0-9]+$/,
    inputErrorMessage: '请输入有效的汉字、数字或英文字符',
  })
    .then(({ value }) => {
      ElMessage({
        type: 'success',
        message: `您输入的内容是：${value}`,
      })
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: '取消输入',
      })
    })
}
</script>