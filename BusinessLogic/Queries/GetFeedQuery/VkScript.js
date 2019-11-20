var actualPosts=[];
if(true) {
    var posts=API.wall
        .get({ owner_id: "-59457168",count: 6 })
        .items;
    var currentTime=API.utils.getServerTime();
    var i=0;
    var j=0;

    while(i<posts.length) {
        var post=posts[i];
        if(post.date+86400*60>currentTime) {
            actualPosts.push(post);
            j=j+1;
        }
        i=i+1;
    }
}

var models=[];
if(true) {
    var i=0;

    while(i<actualPosts.length) {
        var post=actualPosts[i];
        var likesCount=post.likes.count;

        var lastLike=null;
        if(likesCount>0) {
            var likes=API.likes.getList({
                type: "post",
                owner_id: post.owner_id,
                item_id: post.id,
                extended: true,
                filter: "likes",
                skip_own: true,
                count: 1
            });

            var like=likes.items[0];

            lastLike=like.first_name+" "+like.last_name;
        }

        var link="";
        if(true) {
            var fullLink="https://vk.com/wall"+post.owner_id+"_"+post.id;
            var shortLink=API.utils.getShortLink({ url: fullLink }).short_url;

            link=shortLink;
        }

        var imageUrl="";
        if(post.attachments.length>0&&post.attachments[0].type=="photo") {
            var image=post.attachments[0].photo;

            var j=image.sizes.length-1;
            while(j>=0&&imageUrl=="") {
                if(image.sizes[j].width<800) {
                    imageUrl=image.sizes[j].url;
                }
                j=j-1;
            }
        }

        var model={
            Text: post.text,
            Date: post.date,
            Likes: likesCount,
            LastLike: lastLike,
            Link: link,
            ImageUrl: imageUrl
        };
        models.push(model);
        i=i+1;
    }
}

return models;