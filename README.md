# ReqSpec
Requirement specification in Markdown document.

## Requirements

### Create a formated html from Markdown file {#REQ_1 .requirement}
The input markdown file (default is the Readme.md, this file), need to convert to html. In converted html document need to show the <a id="REQ_1.1" class="requirement">requirement id right after the reference in brackets</a>.

### Identify the requirement keys {#REQ_2 .requirement}
Mark the element in Markdown with class __requirement__ which are requirements.

### Unique identifier {#REQ_3 .requirement}
Elements in markdown/html need to have an id attribute with unique value in the project.
If identifier is not unique report error.

### Create list of requirement {#REQ_4 .requirement}
Requirement parser provides an interface to iterate over all requirements.
If the original markdown file contains the "Table of Requirements" element (normaly an empty section), a <a id="REQ_4.1" class="requirement">list of requirement need to be added after</a> this element. The list entries are <a id="REQ_4.2" class="requirement">link to the original requirement</a>, to support easy requirement finding.

## Table of Requirements


