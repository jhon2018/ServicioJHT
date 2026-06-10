const fs = require('fs');
const glob = require('glob');
const files = glob.sync('src/**/*.tsx');

files.forEach(file => {
  let content = fs.readFileSync(file, 'utf8');
  let changed = false;
  
  const regex = /<Grid\s+item([^>]*)>/g;
  content = content.replace(regex, (match, propsStr) => {
    changed = true;
    
    let sizeObj = [];
    const sizeProps = ['xs', 'sm', 'md', 'lg', 'xl'];
    let newPropsStr = propsStr;
    
    sizeProps.forEach(sp => {
      const spRegex = new RegExp((?:\\s|^)={([^}]+)}|(?:\\s|^)=([0-9]+), 'g');
      let matchResult;
      while ((matchResult = spRegex.exec(propsStr)) !== null) {
        const val = matchResult[1] || matchResult[2];
        sizeObj.push(${sp}: );
        newPropsStr = newPropsStr.replace(matchResult[0], '');
      }
    });
    
    let sizeProp = '';
    if (sizeObj.length > 0) {
      sizeProp =  size={{  }};
    }
    
    return <Grid>;
  });
  
  if (changed) {
    fs.writeFileSync(file, content);
    console.log('Fixed', file);
  }
});
