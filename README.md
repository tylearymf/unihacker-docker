# unihacker-docker [English Doc](https://github.com/tylearymf/unihacker-docker/blob/main/README_EN.md)

[UniHacker](https://github.com/tylearymf/UniHacker)

[UnityCI](https://hub.docker.com/r/unityci/editor)

# 前置步骤

### 运行Unity必须指定volume 'opt' 和 'root'

```
docker run -it -v /opt -v /root --name=<你的容器名> unityci/editor:<Unity tag>

如：docker run -it -v /opt -v /root --name=unity-2021.3.13f1 unityci/editor:unityci/editor:2021.3.13f1-base-1.0.1
```

# 使用说明

## 环境变量

* UNITY_PATH

  * Unity的路径，如 'opt/unity'

* EXEC_METHOD

  * 执行哪个方法，如'PATCH、REVERT、CHECK'

  * PATCH
    * 执行破解

  * REVERT
    * 执行还原

  * CHECK
    * 查看状态

## 命令说明

```
docker run --rm -it --name=unihacker --env UNITY_PATH=<Unity路径> --env EXEC_METHOD=<要执行的方法> --volumes-from <Unity容器名> unihacker latest

如：docker run --rm -it --name=unihacker --env UNITY_PATH=/opt/unity --env EXEC_METHOD=PATCH --volumes-from unity-2021.3.13f1 unihacker latest
```

# 注意

最后需要在Unity容器中执行下这个命令

```
chmod +x <Unity路径>/Editor/Unity

如：chmod +x /opt/unity/Editor/Unity
```



