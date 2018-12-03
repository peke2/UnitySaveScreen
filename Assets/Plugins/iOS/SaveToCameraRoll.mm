#import <Foundation/Foundation.h>
#import <AssetsLibrary/AssetsLibrary.h>
#import <AVFoundation/AVFoundation.h>

extern "C" void SaveToCameraRoll(const char *path, const char *cbGameObjectName, const char *cbMethodName)
{
    NSLog(@"--->\n%@\n<---", [NSString stringWithUTF8String:path]);
    UIImage *image = [UIImage imageWithContentsOfFile:[NSString stringWithUTF8String:path]];
    ALAssetsLibrary *library = [[ALAssetsLibrary alloc] init];
    NSMutableDictionary *metadata = [[NSMutableDictionary alloc] init];
    
    //  クロージャと同じだと思って引数を保持して参照したら、コールバックの時点でnullの参照になっていた
    //  メモリ管理が違うのが原因かと思われるので、必要なものをローカルに保持しておく
    NSString *objectName = [NSString stringWithUTF8String:cbGameObjectName];
    NSString *methodName = [NSString stringWithUTF8String:cbMethodName];
    NSString *pathName = [NSString stringWithUTF8String:path];

    [library writeImageToSavedPhotosAlbum:image.CGImage metadata:metadata completionBlock:^(NSURL *assetURL, NSError *error){
        //  いけると思ったけど参照時にnullになっている
        //NSLog(@"GameObject=[%@]", [NSString stringWithUTF8String:cbGameObjectName]);
        //NSLog(@"Method=[%@]", [NSString stringWithUTF8String:cbMethodName]);
        //  設置された範囲内のローカル変数の参照はOK
        NSLog(@"GameObject=[%@]", objectName);
        NSLog(@"Method=[%@]", methodName);
        UnitySendMessage([objectName UTF8String], [methodName UTF8String], [pathName UTF8String]);
    }];
}
