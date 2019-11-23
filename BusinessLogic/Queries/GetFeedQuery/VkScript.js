var actualPosts = [];
if (true) {
  var posts = API.wall
    .get({ owner_id: "-59457168", count: 6 })
    .items;
  var currentTime = API.utils.getServerTime();
  var i = 0;
  var j = 0;
  
  while (i < posts.length) {
    var post = posts[i];
    if (true || post.date + 86400 * 60 > currentTime) {
      if (post.copy_history.length > 0) {
        post.text = post.copy_history[0].text;
        post.attachments = post.copy_history[0].attachments;
      }
      actualPosts.push(post);
      j = j + 1;
    }
    i = i + 1;
  }
}

var models = [];
if (true) {
  var i = 0;
  
  while (i < actualPosts.length) {
    var post = actualPosts[i];
    var likesCount = post.likes.count;
    
    var lastLike = null;
    if (likesCount > 0) {
      var likes = API.likes.getList({ 
        type: "post", 
        owner_id: post.owner_id,
        item_id: post.id,
        extended: false,
        filter: "likes",
        skip_own: true,
        count: 1
      });
      
      var like = likes.items[0];
      var user = API.users.get({
        user_ids: [ like ],
        name_case: "gen"
      })[0];
      
      lastLike = user.first_name + " " + user.last_name;
    }
    
    var text = "";
    if (true) {
      text = post.text;
      
      var i = 0;
      
      while (i < 5) {
        i = i + 1;
        
        var chr1_index = text.indexOf('[');
        var chr2_index = text.indexOf('|');
        var chr3_index = text.indexOf(']');
        
        if (chr1_index < chr2_index && chr1_index < chr3_index) {
          text = text.substr(0, chr1_index) 
            + text.substr(chr2_index + 1, chr3_index - chr2_index - 1)
            + text.substr(chr3_index + 1, text.length);
        }
      }
    }
    
    var imageUrl = "";
    if (post.attachments.length > 0 && post.attachments[0].type == "photo") {
      var image = post.attachments[0].photo;
      
      var j = image.sizes.length - 1;
      while (j >= 0 && imageUrl == "") {
        if (image.sizes[j].width < 800) {
          imageUrl = image.sizes[j].url;
        }
        j = j - 1;
      }
    }
    
    var model = {
      Text: text,
      Date: post.date,
      Likes: likesCount,
      LastLike: lastLike,
      ImageUrl: imageUrl
    };
    models.push(model);
    i = i + 1;
  }
}

return models;