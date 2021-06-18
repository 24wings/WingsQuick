const util = require('../../utils/util.js');
const api = require('../../config/api.js');
const user = require('../../services/user.js');

//获取应用实例
const app = getApp()
Page({
  data: {
    goodsCount: 0,
    newGoods: [],
    hotGoods: [],
    topics: [],
    brands: [],
    floorGoods: [],
    banner: [],
    channel: []
  },
  onShareAppMessage: function () {
    return {
      title: 'NideShop',
      desc: '仿网易严选微信小程序商城',
      path: '/pages/index/index'
    }
  },

  getIndexData: function () {
    let that = this;
    util.request(api.IndexUrl).then(function (res) {
     
        console.log("sssssssssssssss:"+res)

        that.setData({
          newGoods: res.newGoodsList||[],
          hotGoods: res.hotGoodsList||[],
          topics: res.topicList||[],
          brand: res.brandList||[],
          floorGoods: res.categoryList||[],
          banner: res.banner ||[],
          channel: res.channel||[]

        });
      
    });
  },
  onLoad: function (options) {
    this.getIndexData();
    // util.request(api.GoodsCount).then(res => {
    //   this.setData({
    //     goodsCount: res.data.goodsCount
    //   });
    // });
  },
  onReady: function () {
    // 页面渲染完成
  },
  onShow: function () {
    // 页面显示
  },
  onHide: function () {
    // 页面隐藏
  },
  onUnload: function () {
    // 页面关闭
  },
})
