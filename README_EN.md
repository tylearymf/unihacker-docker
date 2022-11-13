# unihacker-docker

[![Docker Stars](https://img.shields.io/docker/stars/tylearymf/unihacker.svg)](https://hub.docker.com/r/tylearymf/unihacker)
[![Docker Pulls](https://img.shields.io/docker/pulls/tylearymf/unihacker.svg)](https://hub.docker.com/r/tylearymf/unihacker)

[UniHacker](https://github.com/tylearymf/UniHacker)

# Docker Image

[unihacker image](https://hub.docker.com/r/tylearymf/unihacker)

[unity image](https://hub.docker.com/r/unityci/editor)

# Precondition

### Running Unity must specify volumes

* volume opt ：Used to find Unity files
* volume root ：Used to generate license files

```
docker run -it -v /opt -v /root --name=<unity container name> unityci/editor:<unity tag>

For example: docker run -it -v /opt -v /root --name=unity-2021.3.13f1 unityci/editor:2021.3.13f1-base-1.0.1
```

# Usage

## Environment Variable

* UNITY_PATH

  * Unity Path，如 'opt/unity'

* EXEC_METHOD

  * Which method to execute, like 'PATCH, REVERT, CHECK'

  * PATCH
    * Crack Unity

  * REVERT
    * Revert Unity

  * CHECK
    * Check Unity

## Command

```
docker run --rm -it --name=unihacker --env UNITY_PATH=<unity path> --env EXEC_METHOD=<execute method> --volumes-from <unity container name> tylearymf/unihacker latest

For example: docker run --rm -it --name=unihacker --env UNITY_PATH=/opt/unity --env EXEC_METHOD=PATCH --volumes-from unity-2021.3.13f1 tylearymf/unihacker latest
```

# Attention

Finally, you need to execute this command in the Unity container

```
chmod +x <unity path>/Editor/Unity

For example: chmod +x /opt/unity/Editor/Unity
```

# Legal Disclaimer

Any use of this software is for non-profit education and testing purposes only.
