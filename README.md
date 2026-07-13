# VideoCutter

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)
![C%23](https://img.shields.io/badge/C%23-239120)
![FFmpeg](https://img.shields.io/badge/FFmpeg-007808)

**VideoCutter** is a C# console application for splitting and processing videos with FFmpeg.

## Features

* splits videos into smaller parts;
* supports resize filters;
* reads settings from JSON;
* processes video parts asynchronously;
* limits the number of parallel tasks;
* supports cancellation with `CancellationToken`;
* uses Dependency Injection and layered architecture.

## Tech Stack

* C#
* .NET 9
* FFmpeg
* FFMpegCore
* System.Text.Json

## Project Structure

```text
VideoCutter/
├── Domain/
├── Application/
├── Infrastructure/
├── VideoCutter/
└── VideoCutter.sln
```

## Configuration

Edit `VideoCutter/config.json` before running the application.

```json
{
  "info": {
    "inputFilePath": "C:\\Videos\\input.mp4",
    "outputFolderPath": "C:\\Videos\\Output"
  },
  "segmentation": {
    "chunkDuration": "00:00:20",
    "offset": "00:00:00"
  }
  "pipelineDefinition": {
    "steps": [
      {
        "type": "Resize",
        "parameters": {          
          "width": "1080",
          "height": "1960"
        }
      }
    ]
  }
}
```

## Run

```bash
git clone https://github.com/BISCUITIC/VideoCutter.git
cd VideoCutter
dotnet restore
dotnet run --project VideoCutter/VideoCutter.csproj
```

## About

This project helped me practice asynchronous programming, multithreading, external process execution, and backend application architecture in .NET.

## License

This project is licensed under the [MIT License](LICENSE).
