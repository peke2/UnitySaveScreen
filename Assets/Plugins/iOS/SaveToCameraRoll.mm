#import <Foundation/Foundation.h>
#import <AssetsLibrary/AssetsLibrary.h>
#import <AVFoundation/AVFoundation.h>

extern "C" void SaveToCameraRoll(const char *path, const char *cbGameObjectName, const char *cbMethodName)
{
    NSLog(@"--->\n%@\n<---", [NSString stringWithUTF8String:path]);
    UIImage *image = [UIImage imageWithContentsOfFile:[NSString stringWithUTF8String:path]];
    ALAssetsLibrary *library = [[ALAssetsLibrary alloc] init];
    NSMutableDictionary *metadata = [[NSMutableDictionary alloc] init];
    [library writeImageToSavedPhotosAlbum:image.CGImage metadata:metadata completionBlock:^(NSURL *assetURL, NSError *error){
       UnitySendMessage(cbGameObjectName, cbMethodName, path);
    }];
}
