Xorshifts
===

## 概要
xorshiftとは、高速で高品質で簡単に乱数を生成するアルゴリズムである。
xorshiftにはいくつかの派生版が存在する
- xorwow : ワイルの定理に基づいて、単純な定数加算をすることで周期を増やす
- xorshift* : 非線形変換として、不可逆な定数を乗算することで、ランダム性を向上する
    - 不可逆な乗算とは、つまりulongに収まらないことが容易に想像できるほど大きな数値を乗算することで桁落ちが発生し、不可逆な変換をすることだと思う
- xorshift+ : 非線形変換として、乗算ではなくて加算を用いる手法

## 利用
CPUで使うならxorshift+  (Google Chromeが2015年に採用)  
GPUで使うならxorwow  (CUDA Toolkit : cuRAND Libraryが採用)  

## 実装済
CPU
- xorshift
- xorwow
- xorshift

GPU
- xorwow

## 参照
xorshift : [G.Marsaglia, Xorshift RNGs, Journal of Statistical Software vol.8-14, 2003.](https://www.jstatsoft.org/article/view/v008i14)

xorshift+ : [S.Vigna, Further scramblings of Marsaglia’s xorshift generators, 2014.](http://vigna.di.unimi.it/ftp/papers/xorshiftplus.pdf)

Wikipedia : [Xorshift](https://en.wikipedia.org/wiki/Xorshift)
（WikipediaとPaperとで、実装が違ったのだが、理由が分からなかったのでWikipediaは参考程度にして、基本実装はPaperに準拠した）

## その他
xorshiftについて、その他の派生や実装について  
[xoshiro / xoroshiro generators and the PRNG shootout](http://xoshiro.di.unimi.it/)

もっと新しいアルゴリズム ： PCG  
[PCG, A Family of Better Random Number Generators](http://www.pcg-random.org/)
