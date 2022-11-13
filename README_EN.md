# unihacker-docker

[![Docker Stars](https://img.shields.io/docker/stars/tylearymf/unihacker.svg)](https://hub.docker.com/r/tylearymf/unihacker)
[![Docker Pulls](https://img.shields.io/docker/pulls/tylearymf/unihacker.svg)](https://hub.docker.com/r/tylearymf/unihacker)

[UniHacker](https://github.com/tylearymf/UniHacker)

# Docker Image

[unihacker image](https://hub.docker.com/r/tylearymf/unihacker)

[unity image](https://hub.docker.com/r/unityci/editor)

# Precondition

## Running Unity must specify volumes

* volume opt ：Used to find Unity files
* volume root ：Used to generate license files

```
1. docker volume create <unity volume name>
2. docker volume create <unity license volume name>
3. docker run -it -v <unity volume name>:/opt -v <unity license volume name>:/root --name=<unity container name> unityci/editor:<unity tag>

For example:
1. docker volume create unity2021.3.12f1-android
2. docker volume create unitylicense
3. docker run -it -v unity2021.3.12f1-android:/opt -v unitylicense:/root --name=unity-2021.3.12f1-android unityci/editor:ubuntu-2021.3.12f1-android-1.0.1
```

## Running UnityHub must specify volumes

* volume opt : Used to find Unity Hub files

```
1. docker volume create <unity hub volume name>
2. docker run -it -v <unity hub volume name>:/opt --name=<Unity container name> unityci/editor:<unity tag>

For example:
1. docker volume create unityhub3.0.0
2. docker run -it -v unityhub3.0.0:/opt --name=unity-hub1 unityci/hub:latest
```

# Usage

## UNITY Environment Variable

* UNITY_PATH

  * Unity file path, e.g. 'opt/unity/Editor/Unity'
  * Default value: none

## UNITY HUB Environment Variable

* HUB_PATH
  * Unity Hub file path, e.g. 'opt/unityhub/unityhub'
  * Default value: none

* NEED_LOGIN
  * Whether to log in ? e.g. 'True' or 'False', ignore case
  * Default value: False
* DISABLE_UPDATE
  * Whether to disable update ? e.g. 'True' or 'False', ignore case
  * Default value: False

## Common Environment Variable

* EXEC_METHOD

  * Which method to execute

  * PATCH
    * Execute crack

  * RESTORE
    * Execute restore

  * CHECK
    * Check status

## UNITY Command

```
docker run --rm -it --name=unihacker --env UNITY_PATH=<unity file path> --env EXEC_METHOD=<execute method> --volumes-from <unity container name> tylearymf/unihacker latest

For example: docker run --rm -it --name=unihacker --env UNITY_PATH=/opt/unity/Editor/Unity --env EXEC_METHOD=PATCH --volumes-from unity-2021.3.13f1 tylearymf/unihacker latest
```

## UNITY HUB Command

```
docker run --rm -it --name=unihacker -e HUB_PATH=<unity hub file path> -e EXEC_METHOD=<execute method> -e NEED_LOGIN=<need login value> -e DISABLE_UPDATE=<disable update value> --volumes-from <unity hub container name> tylearymf/unihacker latest

For example: docker run --rm -it --name=unihacker -e HUB_PATH=/opt/unityhub/unityhub -e EXEC_METHOD=PATCH -e NEED_LOGIN=True -e DISABLE_UPDATE=True --volumes-from unity-hub1 tylearymf/unihacker latest

For example: docker run --rm -it --name=unihacker -e HUB_PATH=/opt/unityhub/unityhub -e EXEC_METHOD=PATCH --volumes-from unity-hub1 tylearymf/unihacker latest
```

# Attention

Finally, you need to execute this command in the Unity container after cracking Unity

```
chmod +x <unity path>/Editor/Unity

For example: chmod +x /opt/unity/Editor/Unity
```

# Legal Disclaimer

Any use of this software is for non-profit education and testing purposes only.
