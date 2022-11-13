# unihacker-docker [English Doc](https://github.com/tylearymf/unihacker-docker/blob/main/README_EN.md)

[![Docker Stars](https://img.shields.io/docker/stars/tylearymf/unihacker.svg)](https://hub.docker.com/r/tylearymf/unihacker)
[![Docker Pulls](https://img.shields.io/docker/pulls/tylearymf/unihacker.svg)](https://hub.docker.com/r/tylearymf/unihacker)

[UniHacker](https://github.com/tylearymf/UniHacker)

# Docker 镜像

[unihacker image](https://hub.docker.com/r/tylearymf/unihacker)

[unity image](https://hub.docker.com/r/unityci/editor)

# 前置步骤

## 运行Unity必须指定volume 

* volume opt ：用于查找 Unity 文件
* volume root ：用于生成许可证文件

```
1. docker volume create <unity volume name>
2. docker volume create <unity license volume name>
3. docker run -it -v <unity volume name>:/opt -v <unity license volume name>:/root --name=<unity container name> unityci/editor:<unity tag>

如：
1. docker volume create unity2021.3.12f1-android
2. docker volume create unitylicense
3. docker run -it -v unity2021.3.12f1-android:/opt -v unitylicense:/root --name=unity-2021.3.12f1-android unityci/editor:ubuntu-2021.3.12f1-android-1.0.1
```

## 运行UnityHub必须指定Volume

* volume opt : 用于查找 Unity Hub 文件

```
1. docker volume create <unity hub volume name>
2. docker run -it -v <unity hub volume name>:/opt --name=<Unity container name> unityci/editor:<unity tag>

如：
1. docker volume create unityhub3.0.0
2. docker run -it -v unityhub3.0.0:/opt --name=unity-hub1 unityci/hub:latest
```

# 使用说明

## UNITY 环境变量

* UNITY_PATH

  * Unity文件路径，如 'opt/unity/Editor/Unity'
* 默认值：无

## UNITY HUB 环境变量

* HUB_PATH
  * Unity Hub文件路径，如 'opt/unityhub/unityhub'
  * 默认值：无
* NEED_LOGIN
  * 是否需要登录，如 'True' 或 'False'，忽略大小写
  * 默认值：False
* DISABLE_UPDATE
  * 是否禁用更新，如 ’True‘ 或 ’False‘，忽略大小写
  * 默认值：False

## 公共环境变量

* EXEC_METHOD

  * 执行哪个方法
  * PATCH
    * 执行破解
  * RESTORE
    * 执行还原
  * CHECK
    * 查看状态

## UNITY 命令说明

```
docker run --rm -it --name=unihacker -e UNITY_PATH=<Unity路径> -e EXEC_METHOD=<要执行的方法> --volumes-from <Unity的容器名> tylearymf/unihacker latest

如：docker run --rm -it --name=unihacker --volumes-from unity-2021.3.12f1-android -e UNITY_PATH=/opt/unity/Editor/Unity -e EXEC_METHOD=PATCH tylearymf/unihacker latest
```

## UNITY HUB 命令说明

```
docker run --rm -it --name=unihacker -e HUB_PATH=<UnityHub路径> -e EXEC_METHOD=<要执行的方法> -e NEED_LOGIN=<是否需要登录> -e DISABLE_UPDATE=<是否禁用更新> --volumes-from <UnityHub的容器名> tylearymf/unihacker latest

如：docker run --rm -it --name=unihacker -e HUB_PATH=/opt/unityhub/unityhub -e EXEC_METHOD=PATCH -e NEED_LOGIN=True -e DISABLE_UPDATE=True --volumes-from unity-hub1 tylearymf/unihacker latest

如：docker run --rm -it --name=unihacker -e HUB_PATH=/opt/unityhub/unityhub -e EXEC_METHOD=PATCH --volumes-from unity-hub1 tylearymf/unihacker latest
```

# 注意

最后，在破解Unity后，你需要在Unity容器中执行下这个命令

```
chmod +x <Unity路径>/Editor/Unity

如：chmod +x /opt/unity/Editor/Unity
```

# 免责声明

本软件的任何使用仅用于非营利性的教育和测试目的。
