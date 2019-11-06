module.exports = {
  pluginOptions: {
    s3Deploy: {
      registry: undefined,
      awsProfile: "default",
      region: "us-west-2",
      bucket: "vue-clientapp",
      createBucket: true,
      staticHosting: true,
      staticIndexPage: "index.html",
      staticErrorPage: "index.html",
      assetPath: "dist",
      assetMatch: "**",
      deployPath: "/",
      acl: "bucket-owner-full-control",
      pwa: false,
      enableCloudfront: false,
      uploadConcurrency: 5,
      pluginVersion: "3.0.0"
    }
  }
};
