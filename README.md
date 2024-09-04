
## 『ASCII: VIDE_2_SCII』 Convert and Play ASCII animation

Lets you convert MP4 files into ASCII animation and be able to play and export thos Animation into a specified binary format

This application is allows you configure how ASCII animation are set-up and I tried to make it as configurable as possible either through the terminal or through code-base

![image](https://github.com/user-attachments/assets/f53ad563-f341-463b-87db-fbe4e4d4f6d7)


## Things to Know before using

This is an old project that I tried to make a year go and only now im able to atleast make it funcitonal

I haven't gone much testing after making this program, and it's still bare-bones atm (some crashes and features)

In this scenario I only tested MP4 files for the actual video converition and WEBM for music files

I also recommended only using lower-quality video (I played around 360p) since longer/high-res videos takes a decent amount of time to convert

## Concept and Dependencies 

**Nuget Used:**
NAUDIO
Accord.Video.FFMPEG

This application uses Accord.Video.FFMPEG for the reading of video files and
The grayscale value is mapped to an index of an array of ASCII characters, where darker shades correspond to specified chracters.

The Program also includes NAudio which allows you to play music files in conjunction with the ASCII Animation
